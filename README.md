#TestCinephiles Xamarin App

Access a list with all upcoming movies. You can select a movie to get more detail about then and search for all movies.

- - -

##App Flow:

1. The ViewModel calls the matching Service Class.
2. The matching Service Class calls the Request Provider.
3  The Request Provider uses the System.Net.Http.HttpClient to make a request to the corresponding API.
4. The Request Provider receives a response from the API and check the return if it's not valid.

- - -

##Built With

Here are the technologies and libraries being used for this app development.

* Xamarin Forms 3.5 (latest version)
* Prism Library for MVVM and Screen Navigation
* DryIoc as dependency injection library (included by Prism Library)
* Xamarin.Essentials to have access to the native apis such as gps, wifi, etc
* Default .Net library (HttpClient) to handle the REST API flow.

I decide use Prsim because for is the easylly libary to contro the MVVM and Navigation of the Screens.
I used the DryIoc for dependency injection that was setup with the prismm too.

