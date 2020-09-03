# Weather Application
Live weather information from weather API app created in MVC 5.2.4. WeatherApp.Web is the start-up project. App is architecture in separate layers so models, services and web are structured in separate C# library projects. There is separate unit test project covering the testing.

# Technology
<ul> 
  <li> .NET Framework 4.6 </li>
  <li> C# </li>
  <li> MVC V5.2 </li>
   <li> Bootstrap 3.3 </li>
  <li> HTML </li>
  <li> CSS </li>
  <li> Calling API using http client </li>
</ul> 

# Methodology  

<ul> 
  <li> Constructor dependency injection</li>
   <li> Abstract class </li>
</ul>
  
# Key Notes
Using the home controller and displaying the user interface screen on View.
User enters the valid location in search box and click on search. 
Home controller uses HttpPost to get weather data from API. 
The business logic is separated in Service layer to call weather API to get live data. 
The service is inherited from abstract class so that in future if we have to call another weather API, separate class can be created.
The app is configured with unique key to access weather API. 
The weather API returns JSON result which is deserilized and mapped into view model.
The app connected to API with .net Http client class. 
The app handles the exceptions, errors and returns user friendly messages. 
The app is using bootstrap v3.0. 

