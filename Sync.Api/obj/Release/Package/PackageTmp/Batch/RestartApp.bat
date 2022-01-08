@echo OFF

cd %windir%\system32\inetsrv

appcmd stop site /site.name:%1

appcmd start site /site.name:%1

exit