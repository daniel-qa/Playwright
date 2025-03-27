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
        Assert.IsTrue((await AutoScript()).Contains("24.22145"));
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

        // 等待 "BMI：" 元素出現，並設置超時時間
        //page.WaitForSelectorAsync("text=BMI：", new WaitForSelectorOptions { Timeout = 60000 });
        
        // 取得包含 "BMI：" 的文字區塊
        var ret = page.TextContentAsync("text=BMI :  ").GetAwaiter().GetResult();
                                                    
        // 回傳取得的區塊
        return ret;
    }
}
