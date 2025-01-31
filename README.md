GitHub User Activity ConsoleApp

Overview

The GitHub User Activity ConsoleApp is a simple command-line interface (CLI) application built using C# .NET. This app allows users to fetch and display recent activity from any GitHub user by utilizing the GitHub API. It retrieves various events, such as commits, issues, and starred repositories, and presents them in a readable format directly in the terminal.

Features

Fetch GitHub User Activity: The application fetches recent events for a given GitHub username, displaying commits, issues, and stars.
Supports Multiple Event Types: It includes several GitHub event types like:
Commits (PushEvent)
Issue Actions (IssuesEvent)
Starred Repositories (WatchEvent)
Error Handling: The app handles errors gracefully, such as invalid usernames or failed API requests, and provides informative error messages.
Asynchronous Requests: The application uses asynchronous HTTP requests for improved performance and responsiveness.
JSON Parsing: Data from the GitHub API is parsed using Newtonsoft.Json to display events in a structured and readable format.

Technologies Used

C# .NET: The application is developed using the C# .NET framework, a versatile and powerful platform for building command-line applications.
GitHub API: Utilized to fetch user event data directly from GitHub.
JSON: Newtonsoft.Json is used for parsing JSON responses from the GitHub API.
