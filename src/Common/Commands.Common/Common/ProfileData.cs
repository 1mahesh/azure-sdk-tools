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

namespace Microsoft.WindowsAzure.Commands.Utilities.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// This class provides the representation of
    /// data loaded and saved into data files
    /// for WindowsAzureProfile.
    /// </summary>
    [DataContract]
    public class ProfileData
    {
        [DataMember]
        public string DefaultEnvironmentName { get; set; }

        [DataMember]
        public IEnumerable<AzureEnvironmentData> Environments { get; set; }

        [DataMember]
        public IEnumerable<AzureSubscriptionData> Subscriptions { get; set; }
    }

    /// <summary>
    /// This class provides the representation of
    /// data loaded and saved into data files for
    /// an individual Azure environment
    /// </summary>
    [DataContract]
    public class AzureEnvironmentData
    {
        /// <summary>
        /// Constructor used by data contract serializer
        /// </summary>
        public AzureEnvironmentData()
        {
        }

        /// <summary>
        /// Helper constructor for converting from in memory object
        /// to serializable one.
        /// </summary>
        /// <param name="inMemoryEnvironment">Environment to serialize data from.</param>

        public AzureEnvironmentData(WindowsAzureEnvironment inMemoryEnvironment)
        {
            Name = inMemoryEnvironment.Name;
            PublishSettingsFileUrl = inMemoryEnvironment.PublishSettingsFileUrl;
            ServiceEndpoint = inMemoryEnvironment.ServiceEndpoint;
            ResourceManagerEndpoint = inMemoryEnvironment.ResourceManagerEndpoint;
            ManagementPortalUrl = inMemoryEnvironment.ManagementPortalUrl;
            StorageEndpointSuffix = inMemoryEnvironment.StorageEndpointSuffix;
            AdTenantUrl = inMemoryEnvironment.ActiveDirectoryEndpoint;
            CommonTenantId = inMemoryEnvironment.ActiveDirectoryCommonTenantId;
            GalleryEndpoint = inMemoryEnvironment.GalleryEndpoint;
            ActiveDirectoryServiceEndpointResourceId = inMemoryEnvironment.ActiveDirectoryServiceEndpointResourceId;
            SqlDatabaseDnsSuffix = inMemoryEnvironment.SqlDatabaseDnsSuffix ?? WindowsAzureEnvironmentConstants.AzureSqlDatabaseDnsSuffix;
        }

        /// <summary>
        /// Helper method to convert to an in-memory environment object.
        /// </summary>
        public WindowsAzureEnvironment ToAzureEnvironment()
        {
            return new WindowsAzureEnvironment
            {
                Name = this.Name,
                PublishSettingsFileUrl = this.PublishSettingsFileUrl,
                ServiceEndpoint = this.ServiceEndpoint,
                ResourceManagerEndpoint = this.ResourceManagerEndpoint,
                ManagementPortalUrl = this.ManagementPortalUrl,
                StorageEndpointSuffix = this.StorageEndpointSuffix,
                ActiveDirectoryEndpoint = this.AdTenantUrl,
                ActiveDirectoryCommonTenantId = this.CommonTenantId,
                GalleryEndpoint = this.GalleryEndpoint,
                ActiveDirectoryServiceEndpointResourceId = this.ActiveDirectoryServiceEndpointResourceId,
                SqlDatabaseDnsSuffix = this.SqlDatabaseDnsSuffix ?? WindowsAzureEnvironmentConstants.AzureSqlDatabaseDnsSuffix,
            };
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string PublishSettingsFileUrl { get; set; }

        [DataMember]
        public string ServiceEndpoint { get; set; }

        [DataMember]
        public string ResourceManagerEndpoint { get; set; }

        [DataMember]
        public string ManagementPortalUrl { get; set; }

        [DataMember]
        public string StorageEndpointSuffix { get; set; }

        [DataMember]
        public string AdTenantUrl { get; set; }

        [DataMember]
        public string CommonTenantId { get; set; }

        [DataMember]
        public string GalleryEndpoint { get; set; }

        [DataMember]
        public string ActiveDirectoryServiceEndpointResourceId { get; set; }

        [DataMember]
        public string SqlDatabaseDnsSuffix { get; set; }
    }

    /// <summary>
    /// This class provides the representation of data loaded
    /// and saved into data file for an individual Azure subscription.
    /// </summary>
    [DataContract]
    public class AzureSubscriptionData
    {
        /// <summary>
        /// Constructor used by DataContractSerializer
        /// </summary>
        public AzureSubscriptionData()
        {
        }

        /// <summary>
        /// Helper constructor to copy data from in memory to serialization format.
        /// </summary>
        /// <param name="inMemorySubscription">The subscription to serialize</param>
        public AzureSubscriptionData(WindowsAzureSubscription inMemorySubscription)
        {
            Name = inMemorySubscription.SubscriptionName;
            SubscriptionId = inMemorySubscription.SubscriptionId;
            ManagementEndpoint = inMemorySubscription.ServiceEndpoint != null ? inMemorySubscription.ServiceEndpoint.ToString() : null;
            ResourceManagerEndpoint = inMemorySubscription.ResourceManagerEndpoint != null ? inMemorySubscription.ResourceManagerEndpoint.ToString() : null;
            ActiveDirectoryEndpoint = inMemorySubscription.ActiveDirectoryEndpoint;
            ActiveDirectoryTenantId = inMemorySubscription.ActiveDirectoryTenantId;
            ActiveDirectoryUserId = inMemorySubscription.ActiveDirectoryUserId;
            ActiveDirectoryServiceEndpointResourceId = inMemorySubscription.ActiveDirectoryServiceEndpointResourceId;
            IsDefault = inMemorySubscription.IsDefault;
            ManagementCertificate = inMemorySubscription.Certificate != null ? inMemorySubscription.Certificate.Thumbprint : null;
            CloudStorageAccount = inMemorySubscription.CurrentStorageAccountName;
            RegisteredResourceProviders = inMemorySubscription.RegisteredResourceProviders;
            GalleryEndpoint = inMemorySubscription.GalleryEndpoint != null ? inMemorySubscription.GalleryEndpoint.ToString() : null;
            SqlDatabaseDnsSuffix = inMemorySubscription.SqlDatabaseDnsSuffix ?? WindowsAzureEnvironmentConstants.AzureSqlDatabaseDnsSuffix;
        }

        /// <summary>
        /// Helper method to convert to an in memory subscription object.
        /// </summary>
        /// <returns>The in memory subscription</returns>
        public WindowsAzureSubscription ToAzureSubscription()
        {
            var result = new WindowsAzureSubscription
            {
                SubscriptionName = this.Name,
                SubscriptionId = this.SubscriptionId,
                ServiceEndpoint = !string.IsNullOrEmpty(ManagementEndpoint) ? new Uri(ManagementEndpoint) : null,
                ResourceManagerEndpoint = !string.IsNullOrEmpty(ResourceManagerEndpoint) ? new Uri(ResourceManagerEndpoint) : null,
                ActiveDirectoryEndpoint = ActiveDirectoryEndpoint,
                ActiveDirectoryTenantId = ActiveDirectoryTenantId,
                ActiveDirectoryUserId = ActiveDirectoryUserId,
                ActiveDirectoryServiceEndpointResourceId = ActiveDirectoryServiceEndpointResourceId,
                IsDefault = this.IsDefault,
                Certificate = !string.IsNullOrEmpty(ManagementCertificate) ? WindowsAzureCertificate.FromThumbprint(ManagementCertificate) : null,
                CurrentStorageAccountName = CloudStorageAccount,
                GalleryEndpoint = !string.IsNullOrEmpty(GalleryEndpoint) ? new Uri(GalleryEndpoint) : null,
                SqlDatabaseDnsSuffix = SqlDatabaseDnsSuffix ?? WindowsAzureEnvironmentConstants.AzureSqlDatabaseDnsSuffix,
            };
            RegisteredResourceProviders = RegisteredResourceProviders ?? new string[0];
            foreach (var resource in RegisteredResourceProviders)
            {
                result.RegisteredResourceProviders.Add(resource);
            }
            return result;
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string SubscriptionId { get; set; }

        [DataMember]
        public string ManagementEndpoint { get; set; }

        [DataMember]
        public string ResourceManagerEndpoint { get; set; }

        [DataMember]
        public string ActiveDirectoryEndpoint { get; set; }

        [DataMember]
        public string ActiveDirectoryTenantId { get; set; }

        [DataMember]
        public string ActiveDirectoryUserId { get; set; }

        [DataMember]
        public string LoginType { get; set; }

        [DataMember]
        public bool IsDefault { get; set; }

        [DataMember]
        public string ManagementCertificate { get; set; }

        [DataMember]
        public string CloudStorageAccount { get; set; }

        [DataMember]
        public IEnumerable<string> RegisteredResourceProviders { get; set; }

        [DataMember]
        public string GalleryEndpoint { get; set; }

        [DataMember]
        public string ActiveDirectoryServiceEndpointResourceId { get; set; }

        [DataMember]
        public string SqlDatabaseDnsSuffix { get; set; }
    }
}