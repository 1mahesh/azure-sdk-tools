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

using Microsoft.Azure.Commands.ResourceManagement.Models;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.ResourceManagement
{
    /// <summary>
    /// Get the list of events for a deployment.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "AzureResourceGroupLog", DefaultParameterSetName = LastDeploymentSetName), OutputType(typeof(List<PSDeploymentEventData>))]
    public class GetAzureResourceGroupLogCommand : ResourceBaseCmdlet
    {
        internal const string AllSetName = "all";
        internal const string LastDeploymentSetName = "lastDeployment";
        internal const string DeploymentNameSetName = "deploymentName";

        [Parameter(Position = 0, ParameterSetName = AllSetName, Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = "Name of the resource group you want to see the logs.")]
        [Parameter(Position = 0, ParameterSetName = LastDeploymentSetName, Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = "Name of the resource group you want to see the logs.")]
        [Parameter(Position = 0, ParameterSetName = DeploymentNameSetName, Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = "Name of the resource group you want to see the logs.")]
        [ValidateNotNullOrEmpty]
        public string ResourceGroupName { get; set; }

        [Parameter(ParameterSetName = DeploymentNameSetName, Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = "Name of the deployment whose logs you want to see.")]
        [ValidateNotNullOrEmpty]
        public string DeploymentName { get; set; }
        
        [Parameter(ParameterSetName = AllSetName, HelpMessage = "Optional. If given, return logs of all the operations including CRUD and deployment.")]
        public SwitchParameter All { get; set; }

        [Parameter(ParameterSetName = LastDeploymentSetName, HelpMessage = "Optional. If given, return logs of the last deployment.")]
        public SwitchParameter LastDeployment { get; set; }

        public override void ExecuteCmdlet()
        {
            GetPSResourceGroupLogParameters parameters = new GetPSResourceGroupLogParameters
                {
                    ResourceGroupName = ResourceGroupName,
                    DeploymentName = DeploymentName,
                    All = All.IsPresent,
                    LastDeployment = LastDeployment.IsPresent
                };
            WriteObject(ResourceClient.GetResourceGroupLogs(parameters), true);
        }
    }
}