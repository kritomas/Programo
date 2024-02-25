# Programo

A simple database thingy for maintaining a database of programmers and their projects, certificates, assignments and stuff.

# Usage

Firstly, configuration is necessary.

This is done by creating a file named `Programo.dll.config` in the same folder as `Programo.dll`, with the following format:

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

Then, you need to setup the database. Log into an **empty** database (no tables whatsoever) with your database management studio of choice, and run `setup.sql`.

The program can then be run from the terminal.

The usage is as follows:

`.\Programo.exe your command here`

The program will tell you which commands are available.