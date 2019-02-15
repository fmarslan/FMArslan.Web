# FMArslan.Web
Simple Multi Language Web Framework on .Net Framework

You can easly make a website for you with razor. 

## Steps
  * You should declare your navigation in App_Data folder.
  * You should add your resource files(css, js, img, font etc) move to Content folder.
  * You can develop your pages in language folders where in Content
  
  ### Example
  
  ``` HTML
@model FMArslan.Web.Model.PageConfig
<div id="header">
    <div>
        <a href="index.html" class="logo"><img src="/Content/images/logo.png" alt=""></a>
        <ul id="navigation">
            @foreach (var page in Model.Navigation.Pages)
                {
                    <li class='menu @(page.Key ==Model.Page.Key ? "selected":"" )'>
                        <a href="/en/@page.Key">@(String.IsNullOrEmpty(page["enTitle"])==false? page["enTitle"] : (String.IsNullOrEmpty(page.Title) == false ? page.Title : page.Key))</a>
                    </li>
            }
        </ul>
    </div>
</div>
  ```

Reference

<a href="https://freewebsitetemplates.com/preview/frozenyogurtshop/index.html" target="_blank">Template 1(freewebsitetemplates/frozenyogurtshop)</a>

<a href="https://startbootstrap.com/themes/creative/" target="_blank">Template 2(startbootstrap)</a>
