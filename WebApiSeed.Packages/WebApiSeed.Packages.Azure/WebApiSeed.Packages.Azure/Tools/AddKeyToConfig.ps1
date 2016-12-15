param($webConfigPath, $key, $value)

$RptKeyFound=0;

$xml = [xml](get-content $webConfigPath);              # Create the XML Object and open the web.config file 
$root = $xml.get_DocumentElement();                     # Get the root element of the file

foreach( $item in $root.appSettings.add)                  # loop through the child items in appsettings 
{ 
    if($item.key –eq $key)                       # If the desired element already exists 
    { 
        $RptKeyFound=1;                                             # Set the found flag 

        write-Output "Key $key already exists in this configuration file."
    } 
}

if($RptKeyFound -eq 0)                                                   # If the desired element does not exist 
{ 
    $newEl=$xml.CreateElement("add");                               # Create a new Element 
    $nameAtt1=$xml.CreateAttribute("key");                         # Create a new attribute “key” 
    $nameAtt1.psbase.value=$key;                    # Set the value of “key” attribute 
    $newEl.SetAttributeNode($nameAtt1);                              # Attach the “key” attribute 
    $nameAtt2=$xml.CreateAttribute("value");                       # Create “value” attribute  
    $nameAtt2.psbase.value=$value;                       # Set the value of “value” attribute 
    $newEl.SetAttributeNode($nameAtt2);                               # Attach the “value” attribute 
    $xml.configuration["appSettings"].AppendChild($newEl);    # Add the newly created element to the right position

    write-Output "Key $key successfully added to configuration file. (value = $value)"
}

$xml.Save($webConfigPath)                                                # Save the web.config file