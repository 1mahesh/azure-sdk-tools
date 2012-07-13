# ----------------------------------------------------------------------------------
#
# Copyright 2011 Microsoft Corporation
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
# http://www.apache.org/licenses/LICENSE-2.0
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
# ----------------------------------------------------------------------------------

[CmdletBinding()]
Param
(
    [Parameter(Mandatory=$true, Position=0)]
    [ValidateNotNullOrEmpty()]
    [string]
    $subscriptionID, 
    [Parameter(Mandatory=$true, Position=1)]
    [ValidateNotNullOrEmpty()]
    [String]
    $SerializedCert,
    [Parameter(Mandatory=$true, Position=2)]
    [ValidateNotNullOrEmpty()]
    [String]
    $serverLocation,
    [Parameter(Mandatory=$true, Position=3)]
    [ValidateNotNullOrEmpty()]
    [String]
    $OutputFile
)
Write-Output "`$subscriptionID=$subscriptionID"
Write-Output "`$SerializedCert=$SerializedCert"
Write-Output "`$serverLocation=$serverLocation"
Write-Output "`$OutputFile=$OutputFile"

. .\CommonFunctions.ps1

Try
{
    Init-TestEnvironment -subscriptionID $subscriptionID -SerializedCert $SerializedCert
    $isTestPass = $False
    
    # Get SqlDatabaseOperationContext format
    $SqlDatabaseOperationContext = New-AzureSqlDatabaseServer -AdministratorLogin "mylogin1" -AdministratorLoginPassword "Sql@zure1" -Location $serverLocation
    $server = $SqlDatabaseOperationContext
    
    # Get SqlDatabaseServerContext format
    $SqlDatabaseServerContext = Get-AzureSqlDatabaseServer $server.ServerName
    
    # Get SqlDatabaseFirewallRuleContext format
    $SqlDatabaseFirewallRuleContext = New-AzureSqlDatabaseServerFirewallRule $server.ServerName -RuleName "test" -StartIpAddress "1.0.0.0" -EndIpAddress "2.0.0.0"
    
    # write the dynamic content in comma separated line
    "$ServerLocation,$($SqlDatabaseOperationContext.ServerName),$($SqlDatabaseOperationContext.OperationId),$($SqlDatabaseServerContext.OperationId),$($SqlDatabaseFirewallRuleContext.OperationId)"  > $OutputFile
    
    # write output object to output file
    $SqlDatabaseOperationContext.GetType().Name >> $OutputFile
    $SqlDatabaseOperationContext >> $OutputFile

    $SqlDatabaseServerContext.GetType().Name >> $OutputFile
    $SqlDatabaseServerContext >> $OutputFile

    $SqlDatabaseFirewallRuleContext.GetType().Name >> $OutputFile
    $SqlDatabaseFirewallRuleContext >> $OutputFile
    
    $isTestPass = $True
}
Finally
{
    if($server)
    {
        # Drop server
        Write-Output "Dropping server $($server.ServerName) ..."
        Remove-AzureSqlDatabaseServer -ServerName $server.ServerName -Force
        Write-Output "Dropped server $($server.ServerName)"
    }
    if($IsTestPass)
    {
        Write-Output "PASS"
    }
    else
    {
        Write-Output "FAILED"
    }
}

