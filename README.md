# UserManagement

This is a common re-usable project for managing users in your .net application


**To add customizable UI, you will need to scaffold the Identity Pages and select which pages you want to customize**<br>
![image](https://github.com/gjones94/UserManagement/assets/141204905/cdc23a13-7937-4eec-a411-fd9052e6e965)


**To add access to the Identity Apis in your own project, you will want to add the following lines of code to your program.cs in your project**<br>
```
// Specific to user of Identity API connection
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<YourDbName>();
```

```
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

**app.MapIdentityApi<IdentityUser>();**

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
```

