$RimworldPath = "C:\Games\Rimworld\RimWorld1-0-2408Win64"

$ExePath = $RimworldPath + "\RimWorldWin64.exe"
$ModDestination = $RimworldPath + "\Mods\ED-EnhancedOptions"

Remove-Item -Path $ModDestination -Recurse

Copy-Item -Path "C:\~Git\Jaxxa-Rimworld\~SubModules\ED-EnhancedOptions\ED-EnhancedOptions" -Destination $ModDestination -Recurse

start -FilePath $ExePath -ArgumentList "-savedatafolder=C:\Games\Rimworld\RimWorld1-0-2408Win64\SaveData"