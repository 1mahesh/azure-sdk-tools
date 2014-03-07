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
    /// Get the available locations for certain resource types.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "AzureLocation"), OutputType(typeof(List<PSResourceProviderType>))]
    public class GetAzureLocationCommand : ResourceBaseCmdlet
    {
        [Parameter(Position = 0, Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = "specifies resource type provider.")]
        [ValidateNotNullOrEmpty]
        public string[] ResourceType { get; set; }

        [Parameter(Position = 1, Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = "specifies resource group provider.")]
        [ValidateNotNullOrEmpty]
        public SwitchParameter ResourceGroup { get; set; }
        
        public override void ExecuteCmdlet()
        {
            List<string> resourceTypes = new List<string>();
            if (ResourceType != null)
            {
                resourceTypes.AddRange(ResourceType);
            }

            if (ResourceGroup.IsPresent)
            {
                resourceTypes.Add(ResourcesClient.ResourcGroupTypeName);
            }

            WriteObject(ResourceClient.GetLocations(resourceTypes.ToArray()), true);
        }
    }
}