## Description ##

ASP.NET Core MVC + EF Core test project. Use only at your own risk. **Do not attempt to use this project's code in a production environment** as it contains major security, performance and overall efficiency flaws. I keep this project here for testing and studying purposes only.

## Test project ##

The goal: Implement simple RSS reader.

### Technologies ###
* ASP.NET MVC 5 + EF 6.0 or ASP.NET Core MVC + EF Core ✅ (Core selected)
* Code first DB models ✅
* GIT for versioning ✅

### Functional requirements ###
* Add RSS feed (url + name) ✅
* List of all feeds ✅
* Delete feed ✅
* Detail of feed ✅
 * List of all articles in the feed ✅
 * Filter articles by date (from-to; use date picker of your choice) ✅
 * Reload articles in feed ✅
 
### Bonus functional requirements ###
* Add checkbox for deleting feeds. Add button for checking all checkboxes.
* Search in feeds (by name) or in articles (by title). ✅
* Users, login etc. ~~Everything will be public.~~
* Cron for reloading feeds. ~~Simple button for reloading one feed will be ok.~~
* Detail of article. External link to the article will be enough. ✅
