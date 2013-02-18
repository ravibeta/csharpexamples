<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="HMKW.Azure" generation="1" functional="0" release="0" Id="58b8e65b-ffba-46a8-ad9e-9ca437144d6b" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="HMKW.AzureGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="HMKW:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/HMKW.Azure/HMKW.AzureGroup/LB:HMKW:Endpoint1" />
          </inToChannel>
        </inPort>
        <inPort name="HMKW:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" protocol="tcp">
          <inToChannel>
            <lBChannelMoniker name="/HMKW.Azure/HMKW.AzureGroup/LB:HMKW:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="Certificate|HMKW:Certificate1" defaultValue="">
          <maps>
            <mapMoniker name="/HMKW.Azure/HMKW.AzureGroup/MapCertificate|HMKW:Certificate1" />
          </maps>
        </aCS>
        <aCS name="Certificate|HMKW:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" defaultValue="">
          <maps>
            <mapMoniker name="/HMKW.Azure/HMKW.AzureGroup/MapCertificate|HMKW:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
          </maps>
        </aCS>
        <aCS name="HMKW:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/HMKW.Azure/HMKW.AzureGroup/MapHMKW:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="HMKW:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" defaultValue="">
          <maps>
            <mapMoniker name="/HMKW.Azure/HMKW.AzureGroup/MapHMKW:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" />
          </maps>
        </aCS>
        <aCS name="HMKW:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" defaultValue="">
          <maps>
            <mapMoniker name="/HMKW.Azure/HMKW.AzureGroup/MapHMKW:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" />
          </maps>
        </aCS>
        <aCS name="HMKW:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" defaultValue="">
          <maps>
            <mapMoniker name="/HMKW.Azure/HMKW.AzureGroup/MapHMKW:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" />
          </maps>
        </aCS>
        <aCS name="HMKW:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" defaultValue="">
          <maps>
            <mapMoniker name="/HMKW.Azure/HMKW.AzureGroup/MapHMKW:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" />
          </maps>
        </aCS>
        <aCS name="HMKW:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" defaultValue="">
          <maps>
            <mapMoniker name="/HMKW.Azure/HMKW.AzureGroup/MapHMKW:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" />
          </maps>
        </aCS>
        <aCS name="HMKWInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/HMKW.Azure/HMKW.AzureGroup/MapHMKWInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:HMKW:Endpoint1">
          <toPorts>
            <inPortMoniker name="/HMKW.Azure/HMKW.AzureGroup/HMKW/Endpoint1" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:HMKW:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput">
          <toPorts>
            <inPortMoniker name="/HMKW.Azure/HMKW.AzureGroup/HMKW/Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </toPorts>
        </lBChannel>
        <sFSwitchChannel name="SW:HMKW:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp">
          <toPorts>
            <inPortMoniker name="/HMKW.Azure/HMKW.AzureGroup/HMKW/Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
          </toPorts>
        </sFSwitchChannel>
      </channels>
      <maps>
        <map name="MapCertificate|HMKW:Certificate1" kind="Identity">
          <certificate>
            <certificateMoniker name="/HMKW.Azure/HMKW.AzureGroup/HMKW/Certificate1" />
          </certificate>
        </map>
        <map name="MapCertificate|HMKW:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" kind="Identity">
          <certificate>
            <certificateMoniker name="/HMKW.Azure/HMKW.AzureGroup/HMKW/Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
          </certificate>
        </map>
        <map name="MapHMKW:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/HMKW.Azure/HMKW.AzureGroup/HMKW/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapHMKW:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" kind="Identity">
          <setting>
            <aCSMoniker name="/HMKW.Azure/HMKW.AzureGroup/HMKW/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" />
          </setting>
        </map>
        <map name="MapHMKW:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" kind="Identity">
          <setting>
            <aCSMoniker name="/HMKW.Azure/HMKW.AzureGroup/HMKW/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" />
          </setting>
        </map>
        <map name="MapHMKW:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" kind="Identity">
          <setting>
            <aCSMoniker name="/HMKW.Azure/HMKW.AzureGroup/HMKW/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" />
          </setting>
        </map>
        <map name="MapHMKW:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" kind="Identity">
          <setting>
            <aCSMoniker name="/HMKW.Azure/HMKW.AzureGroup/HMKW/Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" />
          </setting>
        </map>
        <map name="MapHMKW:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" kind="Identity">
          <setting>
            <aCSMoniker name="/HMKW.Azure/HMKW.AzureGroup/HMKW/Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" />
          </setting>
        </map>
        <map name="MapHMKWInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/HMKW.Azure/HMKW.AzureGroup/HMKWInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="HMKW" generation="1" functional="0" release="0" software="C:\Users\admin\gitrep\csharpexamples\TicTacToeWeb\HMKW.Azure\csx\Debug\roles\HMKW" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
              <inPort name="Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" protocol="tcp" />
              <inPort name="Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp" portRanges="3389" />
              <outPort name="HMKW:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp">
                <outToChannel>
                  <sFSwitchChannelMoniker name="/HMKW.Azure/HMKW.AzureGroup/SW:HMKW:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
                </outToChannel>
              </outPort>
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;HMKW&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;HMKW&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp&quot; /&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
            <storedcertificates>
              <storedCertificate name="Stored0Certificate1" certificateStore="My" certificateLocation="System">
                <certificate>
                  <certificateMoniker name="/HMKW.Azure/HMKW.AzureGroup/HMKW/Certificate1" />
                </certificate>
              </storedCertificate>
              <storedCertificate name="Stored1Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" certificateStore="My" certificateLocation="System">
                <certificate>
                  <certificateMoniker name="/HMKW.Azure/HMKW.AzureGroup/HMKW/Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
                </certificate>
              </storedCertificate>
            </storedcertificates>
            <certificates>
              <certificate name="Certificate1" />
              <certificate name="Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
            </certificates>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/HMKW.Azure/HMKW.AzureGroup/HMKWInstances" />
            <sCSPolicyFaultDomainMoniker name="/HMKW.Azure/HMKW.AzureGroup/HMKWFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyFaultDomain name="HMKWFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="HMKWInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="6a5d6e4a-c9b4-4fdc-bc1a-be72566381ca" ref="Microsoft.RedDog.Contract\ServiceContract\HMKW.AzureContract@ServiceDefinition.build">
      <interfacereferences>
        <interfaceReference Id="ed7d898d-e0d6-4368-b283-57a4d0972c74" ref="Microsoft.RedDog.Contract\Interface\HMKW:Endpoint1@ServiceDefinition.build">
          <inPort>
            <inPortMoniker name="/HMKW.Azure/HMKW.AzureGroup/HMKW:Endpoint1" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="6e9e392f-65a6-4afe-8322-d32351578bbb" ref="Microsoft.RedDog.Contract\Interface\HMKW:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput@ServiceDefinition.build">
          <inPort>
            <inPortMoniker name="/HMKW.Azure/HMKW.AzureGroup/HMKW:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>