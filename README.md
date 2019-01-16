# XForm

<!--
 [![Build Status](https://travis-ci.com/starke0o/XForm.svg?branch=master)](https://travis-ci.com/starke0o/XForm)
-->
[![NuGet](https://img.shields.io/nuget/v/XForm.svg)](https://www.nuget.org/packages/XForm/)


## What is XForm

XForm provides tools to create forms quickly and easily. Simply create a form model and bind it to the provided UI controls.

## Getting started

### Install

Grab the latest [XForm NuGet package](https://www.nuget.org/packages/XForm/) and install in your solution.

TODO: Setup iOS and android

### Create your first form

#### Register XForm


Call in your setup code for andorid:
```csharp
AndroidForm.Register();
```

and for ios:
```csharp
IosForm.Register();
```

#### Define your first form with the FormModel

The form consists of multiple fields. Which fields are displayed is defined by the FormModel. Different field types are available, but it is also possible to create your own field types. 

What a FormModel for a login form could look like:

```csharp
public class LoginFormModel: XForm.Forms.FormModel
{
    public LoginFormModel()
    {
        LoginCommand = new MvxCommand(HandleLoginCommand, IsValid);
    }

    [EmailAddressTextField("E-Mail Address")]
    public string EmailAddress { get; set; }

    [PasswordTextField("Password")]
    public string Password { get; set; }

    [ButtonField("Login")]
    public IMvxCommand LoginCommand { get; }

    protected override void FieldValueChanged()
    {
        LoginCommand.RaiseCanExecuteChanged();
    }

    public bool IsValid()
    {
        return EmailAddress.IsValidEmailAddress() 
            && Password.IsSavePassword();
    }

    public void HandleLoginCommand() 
    {
        // Do login
    }
}
```

The attributes above the properties determine which field is displayed. The following attributes are available:

* LabelFieldAttribute
* Number input
  * DecimalInputFieldAttribute
  * NumberInputFieldAttribute
* Text input
  * SingleLineTextFieldAttribute
  * PasswordTextFieldAttribute
  * EmailAddressTextFieldAttribute
* OptionPickerFieldAttribute
* ButtonFieldAttribute

#### Create the Form from your FormModel

Only two steps are necessary to create the form.

```csharp
// First, instantiate your FormModel
var formModel = new LoginFormModel();

// Second, create the form with the FormModel
Form = Form.Create(formModel);
```

The best place for this is the ViewModel, so the form can be used under both platforms.

#### Display your first Form

With XForm comes the FormView control for the platforms Xamarin.Android and Xamarin.iOS. This control is an extended UITableView respectively RecyclerView. Just add this control to your layout and set (or bind) the FormView's Form property.

## More examples

Complete form examples can be found in the ![Sample Projects](https://github.com/DevelappersGmbH/XForm/tree/master/Sample) folder.

## Contribution

Bug reports and pull requests are welcome.