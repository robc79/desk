# Development diary

## 29-10-2024

### Todo

- [done] (s) Trim unnecessary authentication UI.
- (m) Enable email verification of created accounts.
    - Implemented and sending tested. Cannot test the click link till on production.
- [done] (xs) Remove side links from miniatures pages.
- (l) Add text comments to items.
- (s) View an item.
- (m) Edit an item.
- (xs) Move an item to a different location.
- (m) Paging in the form of prev/next on miniatures pages.
- (l) S3 bucket storage implementation.
- (l) Allow upload of one small image per item once in the tabletop location.
    - Show as a thumbnail on the summary card.
- [done] (xs) Affiliate link banners for Element Games.
- [done] (s) restyle identity pages.
    - [done] Scaffold missing pages.
- [done] (xs) Remove additional email sender flag from config, infer from other one.


### Notes

Pages to scaffold:
    - Account/ForgotPassword
    - Account/ResendEmailConfirmation

Didn't restyle the management pages once signed in though.

## 01-11-2024

### Todo

- (l) Add text comments to items.
- (s) View an item.
- (m) Edit an item.
- (s) Move an item to a different location (state pattern).
- (m) Paging in the form of prev/next on miniatures pages.
- (l) S3 bucket storage implementation.
- (l) Allow upload of one small image per item once in the tabletop location.
    - Show as a thumbnail on the summary card.
- [done] (xs) Toggle registration of new accounts on and off.
- (xs) Configure error pages for common 400, 404, and 500.
- (xs) Investigate query splitting warning from EF Core.
- (xs) Change wording on index page copy when logged in.
- (s) Make messages on miniatures pages responsive to presence/number of items.

### Notes

Tags are proving to be a pain. Feature that came too early? Need to add/remove
them on view of item and also expectation is to search on them.

Do a full "edit" page for item, not the inline thing I've done so far.

## 03-11-2024

### Todo

- (l) Add text comments to items.
- [done] (s) View an item.
- [done] (m) Edit an item.
- (s) Move an item to a different location (state pattern).
- (m) Paging in the form of prev/next on miniatures pages.
- (l) S3 bucket storage implementation.
- (l) Allow upload of one small image per item once in the tabletop location.
    - Show as a thumbnail on the summary card.
- (xs) Configure error pages for common 400, 404, and 500.
- (xs) Investigate query splitting warning from EF Core.
- [done] (xs) Change wording on index page copy when logged in.
- (s) Make messages on miniatures pages responsive to presence/number of items.
- (xs) Display status enum using description.
- (xs) After adding an item, go to the page it was added to instead of just the desk.

### Notes

## 04-11-2024

### Todo

- (m) Add text comments to items.
- (m) Paging in the form of prev/next on miniatures pages.
- (l) S3 bucket storage implementation.
- (l) Allow upload of one small image per item once in the tabletop location.
    - Show as a thumbnail on the summary card.
- (xs) Configure error pages for common 400, 404, and 500.
- (xs) Investigate query splitting warning from EF Core.
- [done] (xs) Change wording on index page copy when logged in.
- (s) Make messages on miniatures pages responsive to presence/number of items.
- [done] (xs) Display status enum using description.
- (xs) After adding an item, go to the page it was added to instead of just the desk.
- [done] (s) Display text comments on item view.
- [done] (s) Add `CreatedOn` and `UpdatedOn` properties to all entities on save.
- [done] (xs) Display created date on comments.

### Notes

## 2024-11-05

### Todo

- [done] (m) Add text comments to items.
- (m) Paging in the form of prev/next on miniatures pages?
- (l) S3 bucket storage implementation.
- (l) Allow upload of one small image per item.
    - Show as a thumbnail on the summary card?
- (s) Configure error pages for 400, 404, and 500.
- [done] (xs) Investigate query splitting warning from EF Core.
- (s) Make messages on miniatures pages responsive to presence/number of items.
- [done] (xs) After adding an item, go to the page it was added to instead of just the desk.
- [done] (m) Clicking on a tag shows list of all item summaries with that tag.

### Notes

Added images to the site. One at the bottom of each page, and another as the site
logo.

Added the `.AsSplitQuery()` call to the loading of a full item to avoid cartesian
explosion issue with single query.

Made tag pills links to tag search page.

## 2024-11-07

### Todo

- (m) Paging in the form of prev/next on miniatures pages?
- (l) S3 bucket storage implementation.
- (l) Allow upload of one small image per item.
- [done] (s) Configure error pages for 400, 404, and 500.
- (s) Make messages on miniatures pages responsive to presence/number of items.
- (s) BUG: AO affiliate image doesn't flex / resize on small screens.
- (xs) Return a 500 error when an error is returned from the application layer.
- (xs) Review unhandled error page logic / format.

### Notes

Site published to the world!

BUG: Consider side drop banners instead of top banner for advertising images.

## 2024-11-08

### Todo

- (m) Paging in the form of prev/next on miniatures pages?
- [done] (m) Wasabi/S3 bucket storage implementation.
- [done] (m) Allow upload of image on item creation.
- (m) Allow changing of image on item edit (replaces the one already there).
- [done] (s) Display image on Item view page.
- (s) Make messages on miniatures pages responsive to presence/number of items.
- [done] (s) BUG: AO affiliate image doesn't flex / resize on small screens.
- [done] (xs) Return a 500 error when an error is returned from the application layer.
- (xs) Review unhandled error page logic / format.
- [done] (xs) Remove privacy link from footer.
- [done] (xs) Limit file upload size on item add item page.
- (xs) Investigate resizing images once submitted to the application.
- (s) Allow removal of an image from an item.
- [done] (xs) Unify layout across all pages.

### Notes

Used fluid styling and auto margins to make banner image flex on resize.

Signed up to Wasabi for S3 bucket access at a fixed price per month. Get a one
month free trial.

Allowing uploads, seems I can only have one handler per page when using a file
upload form. Allow one image attached directly to the miniature. Can be done from
any stage (pile, desk, tabletop).

Added a service interface for Wasabi to `Application` project. New project added
for the implementation `Infrastructure.Wasabi`.

## 2024-11-09

### Todo

- (m) Paging in the form of prev/next on miniatures pages?
- (m) Allow changing of image on item edit (replaces the one already there).
- (s) Make messages on miniatures pages responsive to presence/number of items.
- (xs) Review unhandled error page logic / format.
- (xs) Investigate resizing images once submitted to the application.
- (s) Allow removal of an image from an item.

### Notes

## 2024-11-10

### Todo

- (m) Paging in the form of prev/next on miniatures pages?
- [done] (m) Allow changing of image on item edit (replaces the one already there).
- (s) Make messages on miniatures pages responsive to presence/number of items.
- (xs) Review unhandled error page logic / format.
- (xs) Investigate resizing images once submitted to the application.
- [done] (s) Allow removal of an image from an item.
- (s) Add informational and warning logging to all handlers.
- (xs) Style all buttons using Bootstrap classes.
- (s) Save unhandled exceptions to the database.
- (xs) Look into log shipping to a service / location for analysis.
- (xs) Look into auditing actions in the database.

### Notes

Difficult to diagnose live site errors at present. All the info is in the log,
which you have to download vie FTP to view.

## 2024-11-14

### Todo


- (m) Paging in the form of prev/next on miniatures pages?
- (s) Make messages on miniatures pages responsive to presence/number of items.
- (xs) Review unhandled error page logic / format.
- (xs) Investigate resizing images once submitted to the application.
- (s) Add informational and warning logging to all handlers.
- (xs) Style all buttons using Bootstrap classes.
- (s) Save unhandled exceptions to the database.
- (xs) Look into log shipping to a service / location for analysis.
- (xs) Look into auditing actions in the database.
- (m) Add report page for logged in admin to see number of users, items, and images.
- (s) Add a policy for `admin`, assign it to me in the database.

### Notes

## 2024-11-18

### Todo

- (m) Paging in the form of prev/next on miniatures pages?
- (s) Make messages on miniatures pages responsive to presence/number of items.
- [done] (xs) Review unhandled error page logic / format.
- (xs) Investigate resizing images once submitted to the application.
- (s) Add informational and warning logging to all handlers.
- [done] (xs) Style all buttons using Bootstrap classes.
- (s) Save unhandled exceptions to the database.
- (xs) Look into log shipping to a service / location for analysis.
- (xs) Look into auditing actions in the database.
- [done] (m) Add report page for logged in admin to see number of users, items, and images.
- [done] (xs) Add a policy for `admin`, assign it to me in the database.
- (m) Add image upload for pile, desk, and tabletop.
- (s) Show image if uploaded on pile, desk, and tabletop.
- (l) Unit tests.

### Notes

## 2024-11-20

### Todo

- (m) Paging in the form of prev/next on miniatures pages?
- (s) Make messages on miniatures pages responsive to presence/number of items.
- (xs) Investigate resizing images once submitted to the application.
- (s) Add informational and warning logging to all handlers.
- (s) Save unhandled exceptions to the database.
- (xs) Look into log shipping to a service / location for analysis.
- (xs) Look into auditing actions in the database.
- (m) Add image upload for pile, desk, and tabletop.
- (s) Show image if uploaded on pile, desk, and tabletop.
- (l) Unit tests.
- [done] (s) Rewrite report handler to use a dapper repo impl.

### Notes

## 2024-11-23

### Todo

- (m) Paging in the form of prev/next on miniatures pages?
- (s) Make messages on miniatures pages responsive to presence/number of items.
- (xs) Investigate resizing images once submitted to the application.
- (s) Add informational and warning logging to all handlers.
- (s) Save unhandled exceptions to the database.
- (xs) Look into log shipping to a service / location for analysis.
- (xs) Look into auditing actions in the database.
- (m) Add image upload for pile, desk, and tabletop.
- (s) Show image if uploaded on pile, desk, and tabletop.
- (l) Unit tests.
- [done] (s) Soft delete of items.
- (s) Copy of item.
- [done] (xs) Rename wasabi service interface to `IImageService`.
- [done] (?) Bug on live with user report. Get a 500 page.

### Notes

Copy of item should show the add item screen with the values filled in already,
based on the copied item.

Re-uploaded whole site to fix report error. Not getting logs written out, raise
a support ticket. Sorted, needed to write to the `data` folder up one level.

## 2024-11-24

### Todo

- (m) Paging in the form of prev/next on miniatures pages?
- (s) Make messages on miniatures pages responsive to presence/number of items.
- (xs) Investigate resizing images once submitted to the application.
- [done] (s) Add informational logging to all handlers.
- (s) Save unhandled exceptions to the database.
- (xs) Look into log shipping to a service / location for analysis.
- (xs) Look into auditing actions in the database.
- (m) Add image upload for pile, desk, and tabletop.
- (s) Show image if uploaded on pile, desk, and tabletop.
- (l) Unit tests.
- (s) Copy of item.
- [done] (?) Bug, adding tag when name already exists for any user fails.
- [done] (s) Rename users report to items report.
- (s) Add users report to dump out list of emails with verified status.
- [done] (s) Extend items report to show deleted items.

### Notes

Unique index on tag name is to blame for the bug. Remove it, make the handler
for add tag idempotent.

Bug with registration. Was setting `RequireConfirmedEmail` instead of
`RequireConfirmedAccount`. Doh.

Logging and then unit tests is the order of the day!

## 2024-11-27

### Todo

- (m) Paging in the form of prev/next on miniatures pages?
- (s) Make messages on miniatures pages responsive to presence/number of items.
- (xs) Investigate resizing images once submitted to the application.
- (s) Save unhandled exceptions to the database.
- (xs) Look into log shipping to a service / location for analysis.
- (xs) Look into auditing actions in the database.
- (m) Add image upload for pile, desk, and tabletop.
- (s) Show image if uploaded on pile, desk, and tabletop.
- (l) Unit tests.
- (s) Copy of item.
- (s) Add users report to dump out list of emails with verified status.
- [done] (m) Audit user login and logout.

### Notes

I have a user! When, where, why? Need some auditing in the database.

## 2024-11-28

### Todo

- (m) Paging in the form of prev/next on miniatures pages?
- (s) Make messages on miniatures pages responsive to presence/number of items.
- (xs) Investigate resizing images once submitted to the application.
- (s) Save unhandled exceptions to the database.
- (xs) Look into log shipping to a service / location for analysis.
- (xs) Look into auditing actions in the database.
- (m) Add image upload for pile, desk, and tabletop.
- (s) Show image if uploaded on pile, desk, and tabletop.
- (l) Unit tests.
- (s) Copy of item.
- (s) Add users report to dump out list of emails with verified status.

### Notes
