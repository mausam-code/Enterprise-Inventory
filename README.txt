Enterprise Inventory WPF MVVM Workflow
======================================

This document describes the current MVVM workflow in the EnterpriseInventory.WPF application and 
explains how to add new Views, ViewModels, commands, and navigation paths.

Project structure (MVVM focus)
------------------------------
- `src/EnterpriseInventory.WPF/`
  - `Views/` - UI user controls and windows
  - `ViewModels/` - view models implementing observable state and commands
  - `Services/` - application services used by ViewModels (authentication, navigation, etc.)
  - `Models/` - DTO or domain model classes for UI operations
  - `Infrastructure/` - app infrastructure (database context, data access)
  - `App.xaml.cs` - app startup, bootstrap, initial screen
  - `MainWindow.xaml` - main shell containing navigation and `ContentControl`

Key MVVM files and workflow
---------------------------
1. `App.xaml.cs`
   - Starts the application.
   - Creates `LoginView` and sets `DataContext = new LoginViewModel()`.
   - Shows the login window.

2. `Views/LoginView.xaml`
   - Contains login UI.
   - Binds `TextBox` to `Username` and uses `CommandParameter` to pass the `PasswordBox`.
   - Executes `LoginCommand` on `LoginViewModel`.

3. `ViewModels/LoginViewModel.cs`
   - Implements `ObservableObject` from `CommunityToolkit.Mvvm`.
   - Exposes properties: `Username`, `Password` via `[ObservableProperty]`.
   - Defines `[RelayCommand] private void Login(object parameter)`.
   - Uses `IAuthService` to validate credentials.
   - On success, opens `MainWindow` and closes existing windows.

4. `MainWindow.xaml`
   - Defines the application's shell layout.
   - Sidebar buttons are bound to commands like `ShowDashboardCommand`, `ShowProductsCommand`, `ShowSalesCommand`.
   - Uses `<ContentControl Content="{Binding CurrentView}" />` to render the active page.

5. `ViewModels/MainViewModel.cs`
   - Implements `ObservableObject`.
   - Contains `CurrentView` and an `INavigationService`.
   - Sets the default page to `DashboardView`.
   - Defines relay commands to switch views by setting `CurrentView`.

6. `Views/DashboardView.xaml`, `Views/ProductsView.xaml`, `Views/SalesView.xaml`
   - Each view is a `UserControl` rendered inside `MainWindow`.
   - The shell swaps them by changing `CurrentView` in `MainViewModel`.

7. `Services/Navigation/INavigationService.cs` and `NavigationService.cs`
   - Provides a programmatic way to navigate between views.
   - Current implementation creates view instances and updates `MainViewModel.CurrentView`.

8. `Services/Auth/IAuthService.cs` and `AuthService.cs`
   - Handles login logic.
   - Currently authenticating from the `AppDbContext` database.

9. `ViewModels/BaseViewModel.cs`
   - Base class for view models.
   - Extends `ObservableObject` to support property-change notifications.

How to add a new MVVM feature
-----------------------------
1. Add the View
   - Create a new file in `src/EnterpriseInventory.WPF/Views/`, for example `NewFeatureView.xaml`.
   - Add a matching code-behind `NewFeatureView.xaml.cs`.
   - Keep the view's code-behind minimal: only `InitializeComponent()` and no business logic.

2. Add the ViewModel
   - Create `src/EnterpriseInventory.WPF/ViewModels/NewFeatureViewModel.cs`.
   - Inherit from `BaseViewModel` or `ObservableObject`.
   - Define UI-bound properties with `[ObservableProperty]`.
   - Define actions with `[RelayCommand]`.

3. Bind the View to the ViewModel
   - In the view constructor, set:
     `DataContext = new NewFeatureViewModel();`
   - Or use a DI/host approach later to resolve view models.

4. Expose navigation in `MainViewModel`
   - Add a new relay command, for example `ShowNewFeature()`.
   - Set `CurrentView = new NewFeatureView();` in that command.
   - Add a button in `MainWindow.xaml` bound to `ShowNewFeatureCommand`.

5. Update navigation service (optional / recommended)
   - Extend `INavigationService` to keep view/viewmodel mapping.
   - Instead of using raw control creation in `MainViewModel`, let `NavigationService.NavigateTo<NewFeatureView>()` do the work.
   - Ensure `MainViewModel.CurrentView` is updated after navigation.

6. Add service dependencies
   - If the view model needs shared services, add them to `IAuthService`, `INavigationService`, or create a new service interface.
   - Use constructor injection where possible.
   - If you later enable `Host.CreateDefaultBuilder(...)`, register services in `App.xaml.cs` and resolve view models from the host.

Effective paths and naming conventions
-------------------------------------
- Views: `src/EnterpriseInventory.WPF/Views/{Name}View.xaml` + `.xaml.cs`
- ViewModels: `src/EnterpriseInventory.WPF/ViewModels/{Name}ViewModel.cs`
- Services: `src/EnterpriseInventory.WPF/Services/{Feature}/{Service}.cs`
- Models: `src/EnterpriseInventory.WPF/Models/{Entity}.cs`
- Infrastructure: `src/EnterpriseInventory.WPF/Infrastructure/{SupportingFile}.cs`

Example file mapping
--------------------
- `src/EnterpriseInventory.WPF/Views/CustomerView.xaml`
- `src/EnterpriseInventory.WPF/Views/CustomerView.xaml.cs`
- `src/EnterpriseInventory.WPF/ViewModels/CustomerViewModel.cs`
- `src/EnterpriseInventory.WPF/Services/Customer/CustomerService.cs`
- `src/EnterpriseInventory.WPF/Models/Customer.cs`

Current startup flow
--------------------
1. App launches and `App.OnStartup()` runs.
2. The login screen is shown with `LoginViewModel` as its DataContext.
3. User clicks login and `LoginView()` runs.
4. On successful login, `MainWindow` is created and displayed.
5. `MainWindow` binds to `MainViewModel`.
6. Sidebar navigation buttons switch the current view via `CurrentView`.

Guidelines for your own system design
-------------------------------------
- Keep view code-behind minimal.
- Put all UI state and commands in ViewModels.
- Use `ObservableObject` and `RelayCommand` from `CommunityToolkit.Mvvm`.
- Use `ContentControl` binding to display dynamic pages.
- Keep navigation centralized in `MainViewModel` or a dedicated navigation service.
- Add new pages by creating matching view/viewmodel pairs and wiring commands.

Next improvements you can make
-----------------------------
- Use dependency injection in `App.xaml.cs` to resolve ViewModels and services.
- Migrate `MainViewModel` and `NavigationService` to use typed navigation and view model creation.
- Move `LoginViewModel` and `MainViewModel` from manual `new` construction to host-managed creation.
- Add a `ViewModelLocator` or service provider for cleaner binding in XAML.

This document is intentionally focused on the WPF MVVM workflow.
For domain, application, and infrastructure integration, place shared business logic in the 
corresponding `src/EnterpriseInventory.*` projects and keep WPF focused on UI and presentation.
