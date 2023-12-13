# .NET MAUI sample application 
This sample application is using the following features:
- MVVM
- Entity Framework SQLite
- Commanding
- Asynchronous operations
- CRUD

# MVVM Architecture

Both pages (list and detail) share the same view model.

```mermaid
graph LR
A[ExpenseViewModel] -- View --> B(ExpensesPage)
A -- Create/Update--> C(ExpensePage)
A -- Delete --> D(DeleteCommand)
```


```mermaid
graph LR
A[ExpensesPage] -- Create/Update--> B(ExpensePage)
```
