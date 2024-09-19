В качестве входных аргументов анализатор принимает путь до файла рещения (.sln) или файла проекта (.csproj). В случае указания файла решения, будут проанализированы все проекты, входящие в решение.
Решение включает проект с тестами. Анализатор должен выводить предупреждения в указанный файл при выполнении позитивных тестов и не должен при выполнении негативных тестов соответственно.
В случае проблем со сборкой, заменить '$(DisableMSBuildAssemblyCopyCheck)' != 'true' на '$(DisableMSBuildAssemblyCopyCheck)' == 'true' в файле Microsoft.Build.Locator.targets
![image](https://github.com/user-attachments/assets/f5fe8c07-7c3e-47b8-a581-efd1f42f2959)
