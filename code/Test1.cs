namespace autotest;

using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Playwright;

[TestClass]
public sealed class Test1
{
    [TestMethod]
    public async Task TestMethod1()
    {
        // 運行後的結果包含 24.22145
        //Assert.IsTrue((await AutoScript()).Contains("24.22145"));
        Assert.IsTrue((await AutoScript2()).Contains("每人分配"));
        
    }


    // run auto script
    public static async Task<string> AutoScript2()
    {
        using var playwright = await Playwright.CreateAsync();
        // launch browser
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
        });

        var context = await browser.NewContextAsync();

        // Open new page
        var Page = await context.NewPageAsync();

        //await Page.GotoAsync("https://rc.teammodel.cn/login");

        await Page.GotoAsync("https://www.teammodel.cn/login");
        
        
        await Page.Locator(".login-box").First.ClickAsync();
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Email / 手機號碼 / 用戶編號" }).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Email / 手機號碼 / 用戶編號" }).FillAsync("1629875867");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Email / 手機號碼 / 用戶編號" }).PressAsync("Tab");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "密碼" }).FillAsync("a12345678");
        await Page.Locator("form i").Nth(1).ClickAsync();
        //await Page.GotoAsync("https://rc.teammodel.cn/home/homePage");
        await Page.Locator(".ivu-menu-submenu-title").First.ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = " 教師管理" }).ClickAsync();
        await Page.Locator(".ivu-notice-notice-close").ClickAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "  分配教學空間" }).ClickAsync();


        // 取得包含 "BMI：" 的文字區塊
        var ret = Page.TextContentAsync("text= 每人分配 ").GetAwaiter().GetResult();        

         // 暫停5秒鐘讓你查看頁面
        await Page.WaitForTimeoutAsync(5000); // 暫停5秒

        // 回傳取得的區塊
        return ret;
    }

    // run auto script
    public static async Task<string> AutoScript()
    {
        using var playwright = await Playwright.CreateAsync();
        // launch browser
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
        });

        var context = await browser.NewContextAsync();

        // Open new page
        var page = await context.NewPageAsync();

        // Go to the test website
        await page.GotoAsync("https://td-testplan.azurewebsites.net/");

        // Click input[name="fieldHeight"]
        await page.ClickAsync("input[name=\"fieldHeight\"]");

        // Fill input[name="fieldHeight"]
        await page.FillAsync("input[name=\"fieldHeight\"]", "170");

        // Press Tab
        await page.PressAsync("input[name=\"fieldHeight\"]", "Tab");

        // Fill input[name="fieldWeight"]
        await page.FillAsync("input[name=\"fieldWeight\"]", "70");

        // Click button:has-text("計算")
        await page.ClickAsync("button:has-text(\"計算\")");
        
        // 取得包含 "BMI：" 的文字區塊
        var ret = page.TextContentAsync("text=BMI :  ").GetAwaiter().GetResult();
                                                    
        // 回傳取得的區塊
        return ret;
    }
}
