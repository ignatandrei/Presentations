---
name: search-dilema
description: Search for articles on dilema.ro by submitting a POST request and returning the first 3 article names with links. Use when the user wants to find articles on dilema.ro by keyword or title.
allowed-tools: Bash(playwright-cli:*)
---

# Search dilema.ro Articles

Search for articles on [dilema.ro](https://www.dilema.ro) using the site's search form and return the top 3 results.

## Steps

1. Make a POST request with CURL to `https://www.dilema.ro/search/` with the search POST data in the `s` field. Do not use the GET method or query parameters or playwright.
2. Get the search results page HTML.
3. Extract the HTML, parse and return the **first 3 article names with their links**. ( the links could be relative , not absolute URLs, so make sure to convert them to absolute URLs by prefixing with `https://www.dilema.ro` if necessary).
4. Format the output as a numbered list with article titles and URLs.
5. Ask user what they want to open or if they want to search for something else.
6. If the user wants to open an article, open it with Powershell Start-Process the URL for that article. Also, copy the url to the clipboard with Powershell Set-Clipboard.

## Output format

Return results as a numbered list:

```
1. <Article Title 1> — <URL>
2. <Article Title 2> — <URL>
3. <Article Title 3> — <URL>
```

## Notes

- The search form POSTs to `https://www.dilema.ro/search/` with a field named `s`.
- Article links in results are typically `<a>` tags inside article or result containers.
- If fewer than 3 results are found, return all available results.
- Use `playwright-cli eval` to extract href values if snapshot refs are insufficient.
