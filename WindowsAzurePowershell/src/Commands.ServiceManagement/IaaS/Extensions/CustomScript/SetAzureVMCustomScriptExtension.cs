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

namespace Microsoft.WindowsAzure.Commands.ServiceManagement.IaaS.Extensions
{
    using System;
    using System.Management.Automation;
    using Model;

    [Cmdlet(
        VerbsCommon.Set,
        VirtualMachineCustomScriptExtensionNoun,
        DefaultParameterSetName = SetCustomScriptExtensionParamSetNameByBlobs),
    OutputType(
        typeof(IPersistentVM))]
    public class SetAzureVMCustomScriptExtensionCommand : VirtualMachineCustomScriptExtensionCmdletBase
    {
        protected const string SetCustomScriptExtensionParamSetNameByBlobs = "SetCustomScriptExtensionByContainerAndFileNames";
        protected const string SetCustomScriptExtensionParamSetNameByUris = "SetCustomScriptExtensionByUriLinks";

        [Parameter(
            ParameterSetName = SetCustomScriptExtensionParamSetNameByBlobs,
            Mandatory = true,
            Position = 0,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Name of the Container.")]
        [ValidateNotNullOrEmpty]
        public override string ContainerName { get; set; }

        [Parameter(
            ParameterSetName = SetCustomScriptExtensionParamSetNameByBlobs,
            Mandatory = true,
            Position = 1,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Blob Files in the Container.")]
        [ValidateNotNullOrEmpty]
        public override string[] File { get; set; }

        [Parameter(
            ParameterSetName = SetCustomScriptExtensionParamSetNameByUris,
            Mandatory = false,
            Position = 0,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The File URIs.")]
        [ValidateNotNullOrEmpty]
        public override Uri[] Uri { get; set; }

        [Parameter(
            ParameterSetName = SetCustomScriptExtensionParamSetNameByBlobs,
            Mandatory = false,
            Position = 2,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Command to Execute.")]
        [Parameter(
            ParameterSetName = SetCustomScriptExtensionParamSetNameByUris,
            Mandatory = false,
            Position = 1,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Command to Execute.")]
        [ValidateNotNullOrEmpty]
        public override string Command { get; set; }

        [Parameter(
            ParameterSetName = SetCustomScriptExtensionParamSetNameByBlobs,
            Mandatory = false,
            Position = 3,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Argument for the Command.")]
        [Parameter(
            ParameterSetName = SetCustomScriptExtensionParamSetNameByUris,
            Mandatory = false,
            Position = 2,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Argument for the Command.")]
        [ValidateNotNullOrEmpty]
        public override string[] Argument { get; set; }

        [Parameter(
            ParameterSetName = SetCustomScriptExtensionParamSetNameByBlobs,
            Mandatory = false,
            Position = 4,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The storage account name.")]
        [Parameter(
            ParameterSetName = SetCustomScriptExtensionParamSetNameByUris,
            Mandatory = false,
            Position = 3,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The storage account name.")]
        [ValidateNotNullOrEmpty]
        public override string StorageAccountName { get; set; }

        [Parameter(
            ParameterSetName = SetCustomScriptExtensionParamSetNameByBlobs,
            Mandatory = false,
            Position = 5,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The storage account key.")]
        [Parameter(
            ParameterSetName = SetCustomScriptExtensionParamSetNameByUris,
            Mandatory = false,
            Position = 4,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The storage account key.")]
        [ValidateNotNullOrEmpty]
        public override string StorageAccountKey { get; set; }

        [Parameter(
            ParameterSetName = SetCustomScriptExtensionParamSetNameByBlobs,
            Mandatory = false,
            Position = 6,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Extension Reference Name.")]
        [Parameter(
            ParameterSetName = SetCustomScriptExtensionParamSetNameByUris,
            Mandatory = false,
            Position = 5,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Extension Reference Name.")]
        [ValidateNotNullOrEmpty]
        public override string ReferenceName { get; set; }

        [Parameter(
            ParameterSetName = SetCustomScriptExtensionParamSetNameByBlobs,
            Mandatory = false,
            Position = 7,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Extension Version.")]
        [Parameter(
            ParameterSetName = SetCustomScriptExtensionParamSetNameByUris,
            Mandatory = false,
            Position = 6,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The Extension Version.")]
        [ValidateNotNullOrEmpty]
        public override string Version { get; set; }

        [Parameter(
            ParameterSetName = SetCustomScriptExtensionParamSetNameByBlobs,
            Mandatory = false,
            Position = 8,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Disable VM BGInfo Extension")]
        [Parameter(
            ParameterSetName = SetCustomScriptExtensionParamSetNameByUris,
            Mandatory = false,
            Position = 7,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Disable VM BGInfo Extension")]
        public override SwitchParameter Disable { get; set; }

        internal void ExecuteCommand()
        {
            ValidateParameters();
            RemovePredicateExtensions();
            AddResourceExtension();
            WriteObject(VM);
        }

        protected override void ValidateParameters()
        {
            base.ValidateParameters();
            this.ReferenceName = this.ReferenceName ?? LegacyReferenceName;
            this.PublicConfiguration = GetPublicConfiguration();
            this.PrivateConfiguration = GetPrivateConfiguration();
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            ExecuteCommand();
        }
    }
}
