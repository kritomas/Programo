# Programo

A simple database thingy for programmers and certificates and projects and stuff.

# Usage

Firstly, configuration is necessary.

This is done by creating a file named `App.config`, with the following format:

```
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <appSettings>
    <add key="DataSource" value="[IP address of database server]"/>
    <add key="Database" value="[name of database]"/>
    <add key="Name" value="[username]"/>
    <add key="Password" value="[password]"/>
  </appSettings>
</configuration>
```

The program can then be used from the terminal.

The usage is as follows:

`.\Programo.exe your command here`

The program will tell you which commands are available.