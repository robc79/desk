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
- (l) Wasabi/S3 bucket storage implementation.
- (l) Allow upload of images to log.
- (s) Make messages on miniatures pages responsive to presence/number of items.
- [done] (s) BUG: AO affiliate image doesn't flex / resize on small screens.
- [done] (xs) Return a 500 error when an error is returned from the application layer.
- (xs) Review unhandled error page logic / format.
- [done] (xs) Remove privacy link from footer.

### Notes

Used fluid styling and auto margins to make banner image flex on resize.

Signed up to Wasabi for S3 bucket access at a fixed price per month. Get a one
month free trial.

Allowing uploads, seems I can only have one handler per page when using a file
upload form. Allow one image attached directly to the miniature. Can be done from
any stage (pile, desk, tabletop).

