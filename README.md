# Qrist

![GitHub Release](https://img.shields.io/github/v/release/gman-au/qrist)

## Summary
Qrist is an API / web based tool that performs the following:
* It exposes an API capable of bundling 3rd party integrations into actionable requests
* These requests can be captured in a QR code
* When scanned, Qrist will authenticate with the given 3rd party integration, and process the action on behalf of the logged in account.

## Integrations

### Todoist ([https://www.todoist.com/](https://www.todoist.com/))
Todoist is a task management app that allows users to manage lists of tasks.

Qrist allows the building of QR codes that capture a repeatable set of tasks that can be added in Todoist, for example, adding a QR code to [a printed recipe](https://github.com/gman-au/recipe-formatter) that can add an entire set of ingredients to your shopping list in one simple action.

## Usage
The API can be found at [https://qrist.app](https://qrist.app).

### Generate a QR code (Todoist)
Make a HTTP POST to https://qrist.app/api/build/code using the following JSON format:
```json
{
    "provider": "todoist",
    "data": {
        "tasks": [
            {
                "content": "500g brown mushrooms",
                "description": "(ensure they have stems)",
                "labels": [
                    "shopping list"
                ]
            },
            {
                "content": "1 cup milk",
                "labels": [
                    "shopping list"
                ]
            }
        ]
    }
}
```

If successful, you should be provided with a [base64 string representation](https://www.base64-image.de/tutorial) of the QR code for the Qrist task. 

There are many online tools that can convert this string into an image for you to print out or attach to online documents.

### Scanning the QR code (Todoist)
* When the (above) QR code is scanned, you should be taken to the Qrist site, and redirected to the Todoist authentication endpoint.
* Once authenticated, you should be presented with a confirmation prompt of the list of (tasks) you wish to add via the QR code. Click the **Confirm** button to proceed.
* If successful, you should receive a brief message indicating as such. You can now close the browser window.

> [!NOTE]
> Other than briefly caching sessions for login redirects, Qrist, in its current form, is completely *stateless* in that codes are generated and processed without persistent storage.
>
> QR codes have no 'owner', so any QR codes you create can be given to other users who can also scan them if they have the same 3rd party integration.

## Known limitations
* [QR codes have a size limit](https://en.wikipedia.org/wiki/QR_code#Information_capacity). A given (JSON) request will be compressed to a smaller size and then (decompressed) when scanned on the Qrist site, however there is an upper limit to the amount of data that the request can handle.