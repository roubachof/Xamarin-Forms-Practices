# Xamarin-Forms-Practices
Collection of good practices for Xamarin forms developement in a dangerous async world.

* Android: [![Build status](https://build.appcenter.ms/v0.1/apps/23f44cf3-7656-4932-9d82-f654db6afc82/branches/master/badge)](https://appcenter.ms)
* iOS: [![Build status](https://build.appcenter.ms/v0.1/apps/ddd14409-1f42-4521-ae8d-6f9891de2714/branches/master/badge)](https://appcenter.ms)

Well it's a little bit **Task oriented**, I'm a bit obsessed with view model initialization and Task state management :)

## Why ?

On the Xamarin slack, a question keeps popping:

what is the "good" way of initializing a view model ?

Spoiler alert, this is wrong:

```csharp

public async void Initialize(object parameter)
{
    await InitializationCodeAsync((int)parameter);
}

```

This is a little better:

```csharp

public async void Initialize(object parameter)
{
    try
    {
        await InitializationCodeAsync((int)parameter);
    }
    catch (Exception exception)
    {
        ExceptionHandler.Handle(exception);
    }
}

```

But wait, I want to give a UI feedback to the user:

```csharp

public async void Initialize(object parameter)
{
    IsBusy = true;
    HasErrors = false;
    try
    {
        await InitializationCodeAsync((int)parameter);
    }
    catch (Exception exception)
    {
        ExceptionHandler.Handle(exception);
        HasErrors = true;
        ErrorMessage = 
    }
    finally
    {
        IsBusy = false;
    }
}

```

Pfew, this is a lot of copy paste on each of my VM, I will create a base VM for this, and all my VM will inherit from that.

```
STOP NOW, just stop for the love of will ferrell.
```

**Inheritance** is nice, but don't solve all your code reuse issues with it.

**Composition** is *almost* always better.

## NotifyTask

source: https://github.com/roubachof/Xamarin-Forms-Practices/tree/master/SillyCompany.Mobile.Practices/NotifyTask

Now for the loading part, the issue has been tackled years ago by [Stephen Cleary](https://i.pinimg.com/236x/40/50/80/40508052830e8d54585ee7a83008e00c--monty-python-morals.jpg).
You should use a ```NotifyTask``` object to wrap your async initialization.
It garantees that the exception is correctly caught, and it will notify you (it implements ```INotifyPropertyChanged```).

Start by reading this: https://msdn.microsoft.com/en-us/magazine/dn605875.aspx.
Then go to its github: https://github.com/StephenCleary/Mvvm.Async/tree/master/src/Nito.Mvvm.Async.

The ```NotifyTask``` object has evolved.
It's just a wrapper around a Task, with all the tools you need to give an UI feedback to your users.

I simply added some callbacks to ease the composition, a builder pattern, and some logging to give developper feedback in output window.

## ViewModelLoader

source: https://github.com/roubachof/Xamarin-Forms-Practices/blob/master/SillyCompany.Mobile.Practices/ViewModels/ViewModelLoader.cs

An object wrapping a ```NotifyTask``` (which wraps a ```Task```, you follow ?), filling the gap between pure 
Task UI management and Views data loading scenarios.

It was created for separation of concerns, ```NotifyTask``` must remain async focused while ```ViewModelLoader``` should answer to all 
the needs of the views.

It will handle classic loading patterns:

```
start loading
show loading indicator
end loading 
dismiss indicator
if error: show error view with retry button and corresponding error message
if success: show result
```

It will also handle a Pull-To-Refresh scenario:

1. You initialize your view model, data shows on the view
2. You pull to refresh
3. An error occurs: you don't want to show the error view since you already have data displayed, you just want to display a visual feedback

### TaskLoaderView

source: https://github.com/roubachof/Xamarin-Forms-Practices/tree/master/SillyCompany.Mobile.Practices/Views

This is a container for any view, that will handle the above scenarios.
It is bound to the ```ViewModelLoader``` and will handle the visibility of all the views:

1. Loading spinner view
2. Result view
3. Error view
4. Refresh error view (snackbar-like)

### What ?

To see the ```NotifyTask``` binding in action, have a look at https://github.com/roubachof/Xamarin-Forms-Practices/blob/master/SillyCompany.Mobile.Practices/Views/SillyPeoplePage.xaml.

To see the ```TaskLoaderView```, have a peek at https://github.com/roubachof/Xamarin-Forms-Practices/blob/master/SillyCompany.Mobile.Practices/Views/SillyDudePage.xaml.
