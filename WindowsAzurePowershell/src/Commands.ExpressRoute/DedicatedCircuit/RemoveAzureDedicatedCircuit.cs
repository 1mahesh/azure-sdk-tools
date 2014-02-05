﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
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

namespace Microsoft.WindowsAzure.Commands.ExpressRoute
{
    using System;
    using System.Management.Automation;
    using Utilities.ExpressRoute;
    using Utilities.Properties;
    using Microsoft.WindowsAzure.Management.ExpressRoute.Models;

    [Cmdlet(VerbsCommon.Remove, "AzureDedicatedCircuit")]
    public class RemoveAzureDedicatedCircuitCommand : ExpressRouteBaseCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true,
            HelpMessage = "Service Key of Azure Dedicated Circuit to be removed")]
        [ValidateGuid]
        [ValidateNotNullOrEmpty]
        public string ServiceKey { get; set; }

        [Parameter(HelpMessage = "Do not confirm Azure Dedicated Circuit deletion")]
        public SwitchParameter Force
        {
            get;
            set;
        }

        public override void ExecuteCmdlet()
        {
            ConfirmAction(
                Force.IsPresent,
                string.Format(Resources.RemoveAzureDedicatdCircuitWarning, ServiceKey),
                Resources.RemoveAzureDedicatedCircuitMessage,
                ServiceKey,
                () =>
                    {
                        if (!ExpressRouteClient.RemoveAzureDedicatedCircuit(ServiceKey))
                        {
                            throw new Exception("Remove-AzureDedicatedCircuit Operation failed!");
                        }
                        else
                        {
                            WriteVerboseWithTimestamp("Successfully removed Azure Circuit with service key {0}",ServiceKey);
                        }
                    });
        }
    }
}
