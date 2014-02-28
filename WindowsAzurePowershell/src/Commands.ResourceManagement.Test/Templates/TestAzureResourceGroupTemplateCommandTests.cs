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
using Microsoft.Azure.Commands.ResourceManagement.ResourceGroupDeployments;
using Microsoft.Azure.Management.Resources.Models;
using Moq;
using System.Collections.Generic;
using System.Management.Automation;
using Xunit;

namespace Microsoft.Azure.Commands.ResourceManagement.Test.Resources
{
    public class TestAzureResourceGroupTemplateCommandTests
    {
        private TestAzureResourceGroupTemplateCommand cmdlet;

        private Mock<ResourcesClient> resourcesClientMock;

        private Mock<ICommandRuntime> commandRuntimeMock;

        private string resourceGroupName = "myResourceGroup";

        private string templateFile = @"Resources\sampleTemplateFile.json";

        private string parameterFile = @"Resources\sampleParameterFile.json";

        private string storageAccountName = "myStorageAccount";

        public TestAzureResourceGroupTemplateCommandTests()
        {
            resourcesClientMock = new Mock<ResourcesClient>();
            commandRuntimeMock = new Mock<ICommandRuntime>();
            cmdlet = new TestAzureResourceGroupTemplateCommand()
            {
                CommandRuntime = commandRuntimeMock.Object,
                ResourceClient = resourcesClientMock.Object
            };
        }

        [Fact]
        public void ValidatesPSResourceGroupDeploymentWithUserTemplate()
        {
            ValidatePSResourceGroupDeploymentParameters expectedParameters = new ValidatePSResourceGroupDeploymentParameters()
            {
                ParameterFile = parameterFile,
                TemplateFile = templateFile,
                StorageAccountName = storageAccountName,
                TemplateHash = "hash",
                TemplateHashAlgorithm = "Sha256",
                TemplateVersion = "1.0"
            };
            ValidatePSResourceGroupDeploymentParameters actualParameters = new ValidatePSResourceGroupDeploymentParameters();
            List<ResourceManagementError> expected = new List<ResourceManagementError>()
            {
                new ResourceManagementError()
                {
                    Code = "202",
                    Message = "bad input",
                    Target = "bad target"
                },
                new ResourceManagementError()
                {
                    Code = "203",
                    Message = "bad input 2",
                    Target = "bad target 2"
                },
                new ResourceManagementError()
                {
                    Code = "203",
                    Message = "bad input 3",
                    Target = "bad target 3"
                }
            };
            resourcesClientMock.Setup(f => f.ValidatePSResourceGroupDeployment(
                It.IsAny<ValidatePSResourceGroupDeploymentParameters>()))
                .Returns(expected)
                .Callback((ValidatePSResourceGroupDeploymentParameters p) => { actualParameters = p; });

            cmdlet.ResourceGroupName = resourceGroupName;
            cmdlet.ParameterFile = expectedParameters.ParameterFile;
            cmdlet.TemplateFile = expectedParameters.TemplateFile;
            cmdlet.StorageAccountName = expectedParameters.StorageAccountName;
            cmdlet.TemplateHash = expectedParameters.TemplateHash;
            cmdlet.TemplateHashAlgorithm = expectedParameters.TemplateHashAlgorithm;
            cmdlet.TemplateVersion = expectedParameters.TemplateVersion;

            cmdlet.ExecuteCmdlet();

            Assert.Equal(expectedParameters.GalleryTemplateName, actualParameters.GalleryTemplateName);
            Assert.Equal(expectedParameters.TemplateFile, actualParameters.TemplateFile);
            Assert.Equal(expectedParameters.ParameterObject, actualParameters.ParameterObject);
            Assert.Equal(expectedParameters.ParameterFile, actualParameters.ParameterFile);
            Assert.Equal(expectedParameters.TemplateVersion, actualParameters.TemplateVersion);
            Assert.Equal(expectedParameters.TemplateHash, actualParameters.TemplateHash);
            Assert.Equal(expectedParameters.TemplateHashAlgorithm, actualParameters.TemplateHashAlgorithm);
            Assert.Equal(expectedParameters.StorageAccountName, actualParameters.StorageAccountName);

            commandRuntimeMock.Verify(f => f.WriteObject(expected), Times.Once());
        }

        [Fact]
        public void ValidatesPSResourceGroupDeploymentWithGalleryTemplate()
        {
            ValidatePSResourceGroupDeploymentParameters expectedParameters = new ValidatePSResourceGroupDeploymentParameters()
            {
                ParameterFile = parameterFile,
                GalleryTemplateName = "sqlServer",
                StorageAccountName = storageAccountName,
                TemplateHash = "hash",
                TemplateHashAlgorithm = "Sha256",
                TemplateVersion = "1.0"
            };
            ValidatePSResourceGroupDeploymentParameters actualParameters = new ValidatePSResourceGroupDeploymentParameters();
            List<ResourceManagementError> expected = new List<ResourceManagementError>()
            {
                new ResourceManagementError()
                {
                    Code = "202",
                    Message = "bad input",
                    Target = "bad target"
                },
                new ResourceManagementError()
                {
                    Code = "203",
                    Message = "bad input 2",
                    Target = "bad target 2"
                },
                new ResourceManagementError()
                {
                    Code = "203",
                    Message = "bad input 3",
                    Target = "bad target 3"
                }
            };
            resourcesClientMock.Setup(f => f.ValidatePSResourceGroupDeployment(
                It.IsAny<ValidatePSResourceGroupDeploymentParameters>()))
                .Returns(expected)
                .Callback((ValidatePSResourceGroupDeploymentParameters p) => { actualParameters = p; });

            cmdlet.ResourceGroupName = resourceGroupName;
            cmdlet.ParameterFile = expectedParameters.ParameterFile;
            cmdlet.GalleryTemplateName = expectedParameters.GalleryTemplateName;
            cmdlet.StorageAccountName = expectedParameters.StorageAccountName;
            cmdlet.TemplateHash = expectedParameters.TemplateHash;
            cmdlet.TemplateHashAlgorithm = expectedParameters.TemplateHashAlgorithm;
            cmdlet.TemplateVersion = expectedParameters.TemplateVersion;

            cmdlet.ExecuteCmdlet();

            Assert.Equal(expectedParameters.GalleryTemplateName, actualParameters.GalleryTemplateName);
            Assert.Equal(expectedParameters.TemplateFile, actualParameters.TemplateFile);
            Assert.Equal(expectedParameters.ParameterObject, actualParameters.ParameterObject);
            Assert.Equal(expectedParameters.ParameterFile, actualParameters.ParameterFile);
            Assert.Equal(expectedParameters.TemplateVersion, actualParameters.TemplateVersion);
            Assert.Equal(expectedParameters.TemplateHash, actualParameters.TemplateHash);
            Assert.Equal(expectedParameters.TemplateHashAlgorithm, actualParameters.TemplateHashAlgorithm);
            Assert.Equal(expectedParameters.StorageAccountName, actualParameters.StorageAccountName);

            commandRuntimeMock.Verify(f => f.WriteObject(expected), Times.Once());
        }
    }
}
