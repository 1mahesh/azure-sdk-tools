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

using System;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Management.CloudService.Test;
using Microsoft.WindowsAzure.Management.SqlDatabase.Services;

namespace Microsoft.WindowsAzure.Management.SqlDatabase.Test.UnitTest
{
    /// <summary>
    /// Simple implementation of the <see cref="ISqlDatabaseManagement"/> interface that can be
    /// used for mocking basic interactions without involving Azure directly.
    /// </summary>
    public class SimpleSqlDatabaseManagement : ISqlDatabaseManagement
    {
        /// <summary>
        /// Gets or sets a value indicating whether the thunk wrappers will
        /// throw an exception if the thunk is not implemented.  This is useful
        /// when debugging a test.
        /// </summary>
        public bool ThrowsIfNotImplemented { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleSqlDatabaseManagement"/> class.
        /// </summary>
        public SimpleSqlDatabaseManagement()
        {
            ThrowsIfNotImplemented = true;
        }

        #region Implementation Thunks

        #region GetServers

        public Func<SimpleServiceManagementAsyncResult, SqlDatabaseServerList> GetServersThunk { get; set; }
        public IAsyncResult BeginGetServers(string subscriptionId, AsyncCallback callback, object state)
        {
            SimpleServiceManagementAsyncResult result = new SimpleServiceManagementAsyncResult();
            result.Values["subscriptionId"] = subscriptionId;
            result.Values["callback"] = callback;
            result.Values["state"] = state;
            return result;
        }

        public SqlDatabaseServerList EndGetServers(IAsyncResult asyncResult)
        {
            if (GetServersThunk != null)
            {
                SimpleServiceManagementAsyncResult result = asyncResult as SimpleServiceManagementAsyncResult;
                Assert.IsNotNull(result, "asyncResult was not SimpleServiceManagementAsyncResult!");

                return GetServersThunk(result);
            }
            else if (ThrowsIfNotImplemented)
            {
                throw new NotImplementedException("GetServersThunk is not implemented!");
            }

            return default(SqlDatabaseServerList);
        }

        #endregion

        #region NewServer

        public Func<SimpleServiceManagementAsyncResult, XmlElement> NewServerThunk { get; set; }
        public IAsyncResult BeginNewServer(string subscriptionId, NewSqlDatabaseServerInput input, AsyncCallback callback, object state)
        {
            SimpleServiceManagementAsyncResult result = new SimpleServiceManagementAsyncResult();
            result.Values["subscriptionId"] = subscriptionId;
            result.Values["input"] = input;
            result.Values["callback"] = callback;
            result.Values["state"] = state;
            return result;
        }

        public XmlElement EndNewServer(IAsyncResult asyncResult)
        {
            if (NewServerThunk != null)
            {
                SimpleServiceManagementAsyncResult result = asyncResult as SimpleServiceManagementAsyncResult;
                Assert.IsNotNull(result, "asyncResult was not SimpleServiceManagementAsyncResult!");

                return NewServerThunk(result);
            }
            else if (ThrowsIfNotImplemented)
            {
                throw new NotImplementedException("NewServerThunk is not implemented!");
            }

            return default(XmlElement);
        }

        #endregion

        #region RemoveServer

        public Func<SimpleServiceManagementAsyncResult, SqlDatabaseServerList> RemoveServerThunk { get; set; }
        public IAsyncResult BeginRemoveServer(string subscriptionId, string serverName, AsyncCallback callback, object state)
        {
            SimpleServiceManagementAsyncResult result = new SimpleServiceManagementAsyncResult();
            result.Values["subscriptionId"] = subscriptionId;
            result.Values["serverName"] = serverName;
            result.Values["callback"] = callback;
            result.Values["state"] = state;
            return result;
        }

        public SqlDatabaseServerList EndRemoveServer(IAsyncResult asyncResult)
        {
            if (RemoveServerThunk != null)
            {
                SimpleServiceManagementAsyncResult result = asyncResult as SimpleServiceManagementAsyncResult;
                Assert.IsNotNull(result, "asyncResult was not SimpleServiceManagementAsyncResult!");

                return RemoveServerThunk(result);
            }
            else if (ThrowsIfNotImplemented)
            {
                throw new NotImplementedException("RemoveServerThunk is not implemented!");
            }

            return default(SqlDatabaseServerList);
        }

        #endregion

        #region SetPassword

        public Func<SimpleServiceManagementAsyncResult, SqlDatabaseServerList> SetPasswordThunk { get; set; }
        public IAsyncResult BeginSetPassword(string subscriptionId, string serverName, XmlElement password, AsyncCallback callback, object state)
        {
            SimpleServiceManagementAsyncResult result = new SimpleServiceManagementAsyncResult();
            result.Values["subscriptionId"] = subscriptionId;
            result.Values["serverName"] = serverName;
            result.Values["password"] = password;
            result.Values["callback"] = callback;
            result.Values["state"] = state;
            return result;
        }

        public SqlDatabaseServerList EndSetPassword(IAsyncResult asyncResult)
        {
            if (SetPasswordThunk != null)
            {
                SimpleServiceManagementAsyncResult result = asyncResult as SimpleServiceManagementAsyncResult;
                Assert.IsNotNull(result, "asyncResult was not SimpleServiceManagementAsyncResult!");

                return SetPasswordThunk(result);
            }
            else if (ThrowsIfNotImplemented)
            {
                throw new NotImplementedException("SetPasswordThunk is not implemented!");
            }

            return default(SqlDatabaseServerList);
        }

        #endregion

        #region GetServerFirewallRules

        public Func<SimpleServiceManagementAsyncResult, SqlDatabaseFirewallRulesList> GetServerFirewallRulesThunk { get; set; }
        public IAsyncResult BeginGetServerFirewallRules(string subscriptionId, string serverName, AsyncCallback callback, object state)
        {
            SimpleServiceManagementAsyncResult result = new SimpleServiceManagementAsyncResult();
            result.Values["subscriptionId"] = subscriptionId;
            result.Values["serverName"] = serverName;
            result.Values["callback"] = callback;
            result.Values["state"] = state;
            return result;
        }

        public SqlDatabaseFirewallRulesList EndGetServerFirewallRules(IAsyncResult asyncResult)
        {
            if (GetServerFirewallRulesThunk != null)
            {
                SimpleServiceManagementAsyncResult result = asyncResult as SimpleServiceManagementAsyncResult;
                Assert.IsNotNull(result, "asyncResult was not SimpleServiceManagementAsyncResult!");

                return GetServerFirewallRulesThunk(result);
            }
            else if (ThrowsIfNotImplemented)
            {
                throw new NotImplementedException("GetServerFirewallRulesThunk is not implemented!");
            }

            return default(SqlDatabaseFirewallRulesList);
        }

        #endregion

        #region NewServerFirewallRule

        public Func<SimpleServiceManagementAsyncResult, SqlDatabaseFirewallRulesList> NewServerFirewallRuleThunk { get; set; }
        public IAsyncResult BeginNewServerFirewallRule(string subscriptionId, string serverName, string ruleName, NewSqlDatabaseFirewallRuleInput input, AsyncCallback callback, object state)
        {
            SimpleServiceManagementAsyncResult result = new SimpleServiceManagementAsyncResult();
            result.Values["subscriptionId"] = subscriptionId;
            result.Values["serverName"] = serverName;
            result.Values["ruleName"] = ruleName;
            result.Values["input"] = input;
            result.Values["callback"] = callback;
            result.Values["state"] = state;
            return result;
        }

        public SqlDatabaseFirewallRulesList EndNewServerFirewallRule(IAsyncResult asyncResult)
        {
            if (NewServerFirewallRuleThunk != null)
            {
                SimpleServiceManagementAsyncResult result = asyncResult as SimpleServiceManagementAsyncResult;
                Assert.IsNotNull(result, "asyncResult was not SimpleServiceManagementAsyncResult!");

                return NewServerFirewallRuleThunk(result);
            }
            else if (ThrowsIfNotImplemented)
            {
                throw new NotImplementedException("NewServerFirewallRuleThunk is not implemented!");
            }

            return default(SqlDatabaseFirewallRulesList);
        }

        #endregion

        #region NewServerFirewallRuleWithIpDetect

        public Func<SimpleServiceManagementAsyncResult, XmlElement> NewServerFirewallRuleWithIpDetectThunk { get; set; }
        public IAsyncResult BeginNewServerFirewallRuleWithIpDetect(string subscriptionId, string serverName, string ruleName, AsyncCallback callback, object state)
        {
            SimpleServiceManagementAsyncResult result = new SimpleServiceManagementAsyncResult();
            result.Values["subscriptionId"] = subscriptionId;
            result.Values["serverName"] = serverName;
            result.Values["ruleName"] = ruleName;
            result.Values["callback"] = callback;
            result.Values["state"] = state;
            return result;
        }

        public XmlElement EndNewServerFirewallRuleWithIpDetect(IAsyncResult asyncResult)
        {
            if (NewServerFirewallRuleWithIpDetectThunk != null)
            {
                SimpleServiceManagementAsyncResult result = asyncResult as SimpleServiceManagementAsyncResult;
                Assert.IsNotNull(result, "asyncResult was not SimpleServiceManagementAsyncResult!");

                return NewServerFirewallRuleWithIpDetectThunk(result);
            }
            else if (ThrowsIfNotImplemented)
            {
                throw new NotImplementedException("NewServerFirewallRuleWithIpDetectThunk is not implemented!");
            }

            return default(XmlElement);
        }

        #endregion

        #region RemoveServerFirewallRule

        public Func<SimpleServiceManagementAsyncResult, SqlDatabaseFirewallRulesList> RemoveServerFirewallRuleThunk { get; set; }
        public IAsyncResult BeginRemoveServerFirewallRule(string subscriptionId, string serverName, string ruleName, AsyncCallback callback, object state)
        {
            SimpleServiceManagementAsyncResult result = new SimpleServiceManagementAsyncResult();
            result.Values["subscriptionId"] = subscriptionId;
            result.Values["serverName"] = serverName;
            result.Values["ruleName"] = ruleName;
            result.Values["callback"] = callback;
            result.Values["state"] = state;
            return result;
        }

        public SqlDatabaseFirewallRulesList EndRemoveServerFirewallRule(IAsyncResult asyncResult)
        {
            if (RemoveServerFirewallRuleThunk != null)
            {
                SimpleServiceManagementAsyncResult result = asyncResult as SimpleServiceManagementAsyncResult;
                Assert.IsNotNull(result, "asyncResult was not SimpleServiceManagementAsyncResult!");

                return RemoveServerFirewallRuleThunk(result);
            }
            else if (ThrowsIfNotImplemented)
            {
                throw new NotImplementedException("RemoveServerFirewallRuleThunk is not implemented!");
            }

            return default(SqlDatabaseFirewallRulesList);
        }

        #endregion
     
        #endregion
    }
}

