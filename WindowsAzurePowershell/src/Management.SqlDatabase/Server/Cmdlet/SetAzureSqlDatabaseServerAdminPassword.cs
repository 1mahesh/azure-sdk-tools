﻿// ----------------------------------------------------------------------------------
//
// Copyright 2011 Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

namespace Microsoft.WindowsAzure.Management.SqlDatabase.Server.Cmdlet
{
    using System;
    using System.Management.Automation;
    using System.ServiceModel;
    using Microsoft.WindowsAzure.Management.Extensions;
    using Microsoft.WindowsAzure.Management.SqlDatabase.Model;
    using Microsoft.WindowsAzure.Management.SqlDatabase.Properties;
    using Microsoft.WindowsAzure.Management.SqlDatabase.Services;
    using WAPPSCmdlet = Microsoft.WindowsAzure.Management.CloudService.WAPPSCmdlet;

    /// <summary>
    /// Reset the administrator password for an existing Windows Azure SQL Database server in the selected subscription.
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "AzureSqlDatabaseServerAdminPassword", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
    public class SetAzureSqlDatabaseServerAdminPassword : SqlDatabaseManagementCmdletBase
    {
        public SetAzureSqlDatabaseServerAdminPassword()
        {
        }

        public SetAzureSqlDatabaseServerAdminPassword(ISqlDatabaseManagement channel)
        {
            this.Channel = channel;
        }

        [Parameter(Position = 0, Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = "SQL Database server name.")]
        [ValidateNotNullOrEmpty]
        public string ServerName
        {
            get;
            set;
        }

        [Parameter(Mandatory = true, HelpMessage = "SQL Database administrator login password.")]
        [ValidateNotNullOrEmpty]
        public string NewPassword
        {
            get;
            set;
        }

        [Parameter(HelpMessage = "Do not confirm on the change of administrator login password for the server")]
        public SwitchParameter Force
        {
            get;
            set;
        }

        internal SqlDatabaseServerOperationContext SetAzureSqlDatabaseServerAdminPasswordProcess(string serverName, string newPassword)
        {
            // Do nothing if force is not specified and user cancelled the operation
            if (!Force.IsPresent &&
                !ShouldProcess(string.Empty, string.Format(Resources.SetAzureSqlDatabaseServerAdminPasswordWarning, serverName),
                               Resources.ShouldProcessCaption))
            {
                return null;
            }

            SqlDatabaseServerOperationContext operationContext = null;
            try
            {
                InvokeInOperationContext(() =>
                {
                    RetryCall(subscription =>
                        Channel.SetPassword(subscription, serverName, newPassword));
                    WAPPSCmdlet.Operation operation = WaitForSqlDatabaseOperation();

                    operationContext = new SqlDatabaseServerOperationContext()
                    {
                        ServerName = serverName,
                        OperationDescription = CommandRuntime.ToString(),
                        OperationStatus = operation.Status,
                        OperationId = operation.OperationTrackingId
                    };
                });
            }
            catch (CommunicationException ex)
            {
                this.WriteErrorDetails(ex);
            }

            return operationContext;
        }

        protected override void ProcessRecord()
        {
            try
            {
                base.ProcessRecord();
                SqlDatabaseServerOperationContext context = this.SetAzureSqlDatabaseServerAdminPasswordProcess(this.ServerName, this.NewPassword);

                if (context != null)
                {
                    WriteObject(context, true);
                }
            }
            catch (Exception ex)
            {
                SafeWriteError(new ErrorRecord(this.ProcessExceptionDetails(ex), string.Empty, ErrorCategory.CloseError, null));
            }
        }
    }
}
