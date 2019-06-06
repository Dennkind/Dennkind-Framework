# Dennkind-Framework

## Description
<b>The Dennkind Framework library provides frame and navigation controls for WPF applications.</b><br /><br />
![alt text](http://kronos-server.online/files/dennkindFrameworkDemoApp.png)

## Usage
1. Add the Dennkind.Framework.WPF.Controls namespace:<br />
<code>xmlns:dennkind="clr-namespace:Dennkind.Framework.WPF.Controls;assembly=Dennkind.Framework</code>

2. Implement the Dennkind ApplicationFrameControl:<br />
<code><dennkind:ApplicationFrameControl x:Name="applicationFrameControl" /></code>

3. Initialize the ApplicationFrameControl in XAML or in Code-Behind:<br />
<code>applicationFrameControl.HeaderLogo = "Dennkind";</code><br />
<code>applicationFrameControl.HeaderTitle = "Framework Demo";</code><br />
<code>applicationFrameControl.FooterVersion = "Dennkind Framework Demo v.1.0.0";</code><br />
<code>applicationFrameControl.FooterCopyright = "Copyright © Lukas Koch 2019";</code>

4. Add your pages to the ApplicationFrameControl:<br />
<code>applicationFrameControl.AddPage(new DashboardPage(), dashboardIcon);</code><br />
<code>applicationFrameControl.AddPage(new DocumentsPage(), documentsIcon);</code>

5. Display any page by using the name:<br />
<code>applicationFrameControl.DisplayPage("dashboardPage");</code>

## Demo App
Try the demo app: [Dennkind Framework Demo App](https://github.com/Dennkind/Dennkind-Framework-Demo)

## NuGet Packages
Consume the [Dennkind Framework as NuGet](https://www.nuget.org/packages/Dennkind.Framework/1.0.0):<br />

### Version 1.0.0 (Release):
https://www.nuget.org/packages/Dennkind.Framework/1.0.0

## Changelog:
### Version 1.1.0 (Release) [In Progress](https://github.com/Dennkind/Dennkind-Framework/projects/2):
- Animations added to the header, navigation, content and footer controls.

### Version 1.0.0 (Release):
- ApplicationFrameControl
- HeaderControl
- NavigationControl
- ContentControl
- FooterControl

## Dennkind Framework Trello Board:
- [link to Dennkind Framework Trello Board](https://trello.com/b/RbvKbD10/dennkind-framework) 
