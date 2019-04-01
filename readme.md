# Synonyms.Test.Web

This is .Net Core web application that provides backend APIs for UWP Synonyms client
It uses cache for storing data so in case when it is hosted on Azure and it's VM gets shut down due to inactivity, the data will be lost.

Running the application can be done easily trough Visual Studio by choosing `Start Debugging` from `Debug` menu or by pressing F5 button
It can also be run from docker by executing `docker-componse` command