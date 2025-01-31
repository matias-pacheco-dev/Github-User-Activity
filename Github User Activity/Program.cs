using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

class GitHubActivityFetcher
{

    static async Task Main(string[] args)
    {
        // Prompt the user for the GitHub username
        Console.WriteLine("Enter the GitHub username: ");
        string username = Console.ReadLine();

        // If the username is empty, display a message and exit
        if (string.IsNullOrEmpty(username))
        {
            Console.WriteLine("Username cannot be empty.");
            return;
        }

        // Call the method to fetch and display GitHub activity
        await FetchGitHubActivity(username);
    }


    static async Task FetchGitHubActivity(string username)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                // GitHub API URL to fetch the user's events
                string url = $"https://api.github.com/users/{username}/events";

                // Set the 'User-Agent' header to comply with GitHub API requirements
                client.DefaultRequestHeaders.UserAgent.ParseAdd("CSharpApp");

                // Perform the GET request to GitHub API
                HttpResponseMessage response = await client.GetAsync(url);

                // If the response is successful, process and display the activity
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    DisplayActivity(content);
                }
                else
                {
                    // Display an error message if data couldn't be fetched
                    Console.WriteLine($"Error: Unable to fetch data for user {username}. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Catch any error that occurs during the request and display the message
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    static void DisplayActivity(string json)
    {
        // If the JSON is empty, display a message
        if (string.IsNullOrEmpty(json))
        {
            Console.WriteLine("No activity found for this user.");
            return;
        }

        // Parse the JSON using Newtonsoft.Json (JArray.Parse converts the string to a JSON array)
        var events = JArray.Parse(json);

        // Display a header for the activity list
        Console.WriteLine("\nRecent Activity:");
        Console.WriteLine("------------------");

        // Iterate over each event and display it in a readable format
        foreach (var activity in events)
        {
            string eventType = activity["type"].ToString(); // Event type (PushEvent, IssuesEvent, etc.)
            string repoName = activity["repo"]["name"].ToString(); // Name of the repository associated with the activity

            // Depending on the event type, display specific information
            switch (eventType)
            {
                case "PushEvent":
                    // Display the number of commits made to the repository
                    int commitCount = activity["payload"]["commits"].Count();
                    Console.WriteLine($"Pushed {commitCount} commit(s) to {repoName}");
                    break;

                case "IssuesEvent":
                    // Display if an issue was opened or closed
                    string action = activity["payload"]["action"].ToString();
                    Console.WriteLine($"Issue {action} in {repoName}");
                    break;

                case "WatchEvent":
                    // Display if the repository was starred
                    Console.WriteLine($"Starred {repoName}");
                    break;

                default:
                    // Display any other type of event
                    Console.WriteLine($"{eventType} on {repoName}");
                    break;
            }
        }
    }
}
