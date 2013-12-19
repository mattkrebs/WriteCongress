<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="WebRole" generation="1" functional="0" release="0" Id="f77f5cdc-992f-425a-bea2-03a32d3494fc" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="WebRoleGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="WriteCongress.Web:HTTP" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/WebRole/WebRoleGroup/LB:WriteCongress.Web:HTTP" />
          </inToChannel>
        </inPort>
        <inPort name="WriteCongress.Web:HTTPS" protocol="https">
          <inToChannel>
            <lBChannelMoniker name="/WebRole/WebRoleGroup/LB:WriteCongress.Web:HTTPS" />
          </inToChannel>
        </inPort>
        <inPort name="WriteCongress.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" protocol="tcp">
          <inToChannel>
            <lBChannelMoniker name="/WebRole/WebRoleGroup/LB:WriteCongress.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="Certificate|WriteCongress.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" defaultValue="">
          <maps>
            <mapMoniker name="/WebRole/WebRoleGroup/MapCertificate|WriteCongress.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
          </maps>
        </aCS>
        <aCS name="Certificate|WriteCongress.Web:SSL" defaultValue="">
          <maps>
            <mapMoniker name="/WebRole/WebRoleGroup/MapCertificate|WriteCongress.Web:SSL" />
          </maps>
        </aCS>
        <aCS name="WriteCongress.Web:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/WebRole/WebRoleGroup/MapWriteCongress.Web:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="WriteCongress.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" defaultValue="">
          <maps>
            <mapMoniker name="/WebRole/WebRoleGroup/MapWriteCongress.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" />
          </maps>
        </aCS>
        <aCS name="WriteCongress.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" defaultValue="">
          <maps>
            <mapMoniker name="/WebRole/WebRoleGroup/MapWriteCongress.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" />
          </maps>
        </aCS>
        <aCS name="WriteCongress.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" defaultValue="">
          <maps>
            <mapMoniker name="/WebRole/WebRoleGroup/MapWriteCongress.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" />
          </maps>
        </aCS>
        <aCS name="WriteCongress.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" defaultValue="">
          <maps>
            <mapMoniker name="/WebRole/WebRoleGroup/MapWriteCongress.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" />
          </maps>
        </aCS>
        <aCS name="WriteCongress.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" defaultValue="">
          <maps>
            <mapMoniker name="/WebRole/WebRoleGroup/MapWriteCongress.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" />
          </maps>
        </aCS>
        <aCS name="WriteCongress.WebInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/WebRole/WebRoleGroup/MapWriteCongress.WebInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:WriteCongress.Web:HTTP">
          <toPorts>
            <inPortMoniker name="/WebRole/WebRoleGroup/WriteCongress.Web/HTTP" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:WriteCongress.Web:HTTPS">
          <toPorts>
            <inPortMoniker name="/WebRole/WebRoleGroup/WriteCongress.Web/HTTPS" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:WriteCongress.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput">
          <toPorts>
            <inPortMoniker name="/WebRole/WebRoleGroup/WriteCongress.Web/Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </toPorts>
        </lBChannel>
        <sFSwitchChannel name="SW:WriteCongress.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp">
          <toPorts>
            <inPortMoniker name="/WebRole/WebRoleGroup/WriteCongress.Web/Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
          </toPorts>
        </sFSwitchChannel>
      </channels>
      <maps>
        <map name="MapCertificate|WriteCongress.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" kind="Identity">
          <certificate>
            <certificateMoniker name="/WebRole/WebRoleGroup/WriteCongress.Web/Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
          </certificate>
        </map>
        <map name="MapCertificate|WriteCongress.Web:SSL" kind="Identity">
          <certificate>
            <certificateMoniker name="/WebRole/WebRoleGroup/WriteCongress.Web/SSL" />
          </certificate>
        </map>
        <map name="MapWriteCongress.Web:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/WebRole/WebRoleGroup/WriteCongress.Web/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapWriteCongress.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" kind="Identity">
          <setting>
            <aCSMoniker name="/WebRole/WebRoleGroup/WriteCongress.Web/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" />
          </setting>
        </map>
        <map name="MapWriteCongress.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" kind="Identity">
          <setting>
            <aCSMoniker name="/WebRole/WebRoleGroup/WriteCongress.Web/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" />
          </setting>
        </map>
        <map name="MapWriteCongress.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" kind="Identity">
          <setting>
            <aCSMoniker name="/WebRole/WebRoleGroup/WriteCongress.Web/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" />
          </setting>
        </map>
        <map name="MapWriteCongress.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" kind="Identity">
          <setting>
            <aCSMoniker name="/WebRole/WebRoleGroup/WriteCongress.Web/Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" />
          </setting>
        </map>
        <map name="MapWriteCongress.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" kind="Identity">
          <setting>
            <aCSMoniker name="/WebRole/WebRoleGroup/WriteCongress.Web/Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" />
          </setting>
        </map>
        <map name="MapWriteCongress.WebInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/WebRole/WebRoleGroup/WriteCongress.WebInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="WriteCongress.Web" generation="1" functional="0" release="0" software="C:\Personal\WriteCongress\WebRole\csx\Staging\roles\WriteCongress.Web" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="768" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="HTTP" protocol="http" portRanges="80" />
              <inPort name="HTTPS" protocol="https" portRanges="443">
                <certificate>
                  <certificateMoniker name="/WebRole/WebRoleGroup/WriteCongress.Web/SSL" />
                </certificate>
              </inPort>
              <inPort name="Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" protocol="tcp" />
              <inPort name="Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp" portRanges="3389" />
              <outPort name="WriteCongress.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp">
                <outToChannel>
                  <sFSwitchChannelMoniker name="/WebRole/WebRoleGroup/SW:WriteCongress.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
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
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;WriteCongress.Web&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;WriteCongress.Web&quot;&gt;&lt;e name=&quot;HTTP&quot; /&gt;&lt;e name=&quot;HTTPS&quot; /&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp&quot; /&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
            <storedcertificates>
              <storedCertificate name="Stored0Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" certificateStore="My" certificateLocation="System">
                <certificate>
                  <certificateMoniker name="/WebRole/WebRoleGroup/WriteCongress.Web/Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
                </certificate>
              </storedCertificate>
              <storedCertificate name="Stored1SSL" certificateStore="My" certificateLocation="System">
                <certificate>
                  <certificateMoniker name="/WebRole/WebRoleGroup/WriteCongress.Web/SSL" />
                </certificate>
              </storedCertificate>
            </storedcertificates>
            <certificates>
              <certificate name="Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
              <certificate name="SSL" />
            </certificates>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/WebRole/WebRoleGroup/WriteCongress.WebInstances" />
            <sCSPolicyUpdateDomainMoniker name="/WebRole/WebRoleGroup/WriteCongress.WebUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/WebRole/WebRoleGroup/WriteCongress.WebFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="WriteCongress.WebUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="WriteCongress.WebFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="WriteCongress.WebInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="5d37ab72-4e1a-492c-acc8-f43900c63c1e" ref="Microsoft.RedDog.Contract\ServiceContract\WebRoleContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="b1baa98c-4123-4d18-9b88-fcc5bc2aa4bc" ref="Microsoft.RedDog.Contract\Interface\WriteCongress.Web:HTTP@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/WebRole/WebRoleGroup/WriteCongress.Web:HTTP" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="3b06610a-71ae-4c6a-b39c-9d73ba534505" ref="Microsoft.RedDog.Contract\Interface\WriteCongress.Web:HTTPS@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/WebRole/WebRoleGroup/WriteCongress.Web:HTTPS" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="fc1e689e-efac-45d2-a915-c6a8d9d886a5" ref="Microsoft.RedDog.Contract\Interface\WriteCongress.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/WebRole/WebRoleGroup/WriteCongress.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>