# Dennkind-Framework

## Description
<b>The Dennkind Framework library provides animated frame and navigation controls for WPF applications.</b>

![Dennkind Framework Demo App](http://www.dennkind.com/files/dennkindFrameworkDemoApp.png)

## Usage
1. Add the Dennkind.Framework.WPF.Controls namespace:<br />
<code>xmlns:dennkind="clr-namespace:Dennkind.Framework.WPF.Controls;assembly=Dennkind.Framework</code>

2. Implement the Dennkind ApplicationFrameControl within your application window:<br />
<code><dennkind:ApplicationFrameControl x:Name="applicationFrameControl" /></code>

3. Initialize the ApplicationFrameControl in XAML or in Code-Behind:<br />
<code>applicationFrameControl.HeaderLogo = "Dennkind";</code><br />
<code>applicationFrameControl.HeaderTitle = "Framework Demo";</code><br />
<code>applicationFrameControl.FooterVersion = "Dennkind Framework Demo v.1.0.0";</code><br />
<code>applicationFrameControl.FooterCopyright = "Copyright © Lukas Koch 2019";</code>

4. Add your pages to the ApplicationFrameControl:<br />
<code>applicationFrameControl.AddPage(new DashboardPage(){ Name = "dashboardPage" }, dashboardIcon);</code><br />
<code>applicationFrameControl.AddPage(new DocumentsPage(){ Name = "documentsPage" }, documentsIcon);</code>

5. Display any page by using the name:<br />
<code>applicationFrameControl.DisplayPage("dashboardPage");</code>

### Animations 
Hide controls on startup by setting the IsShown property to false:<br />
<code>applicationFrameControl.Header.IsShown = false;</code><br />
<code>applicationFrameControl.Footer.IsShown = false;</code><br />
<code>applicationFrameControl.Content.IsShown = false;</code><br />
<code>applicationFrameControl.Navigation.IsShown = false;</code><br />

Display controls by calling the FadeIn method:<br />
<code>applicationFrameControl.Header.FadeIn();</code><br />
<code>applicationFrameControl.Footer.FadeIn();</code><br />
<code>applicationFrameControl.Content.FadeIn();</code><br />
<code>applicationFrameControl.Navigation.FadeIn();</code><br />

Hide the controls by calling the FadeOut method:<br />
<code>applicationFrameControl.Header.FadeOut();</code><br />
<code>applicationFrameControl.Footer.FadeOut();</code><br />
<code>applicationFrameControl.Content.FadeOut();</code><br />
<code>applicationFrameControl.Navigation.FadeOut();</code><br />

## Demo App
Try the demo app: [Dennkind Framework Demo App](https://github.com/Dennkind/Dennkind-Framework-Demo) (outdated: new version comming soon)

## NuGet Packages
Consume the [Dennkind Framework as NuGet](https://www.nuget.org/packages/Dennkind.Framework/1.2.0):<br />

### Version 1.2.0 (Release):
https://www.nuget.org/packages/Dennkind.Framework/1.2.0

## Changelog:
### Version 1.2.0 (Release):
- New controls added: DialogControl, OverlayControl, ProgressControl and SplashscreenControl

### Version 1.1.0 (Release):
- Animations added to the header, navigation, content and footer controls.

### Version 1.0.0 (Release):
- ApplicationFrameControl
- HeaderControl
- NavigationControl
- ContentControl
- FooterControl

## Dennkind Framework Trello Board:
- [link to Dennkind Framework Trello Board](https://trello.com/b/RbvKbD10/dennkind-framework) 
