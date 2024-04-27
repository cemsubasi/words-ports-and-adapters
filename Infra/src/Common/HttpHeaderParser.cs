using System.Text.Json;

namespace Infra.Common;

// User-Agent: <product> / <product-version> <comment>
// User-Agent: Mozilla/5.0 (<system-information>) <platform> (<platform-details>) <extensions>
// Mozilla/5.0 (platform; rv:geckoversion) Gecko/geckotrail Firefox/firefoxversion

// Firefox UA string
// Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0
// Mozilla/5.0 (Macintosh; Intel Mac OS X x.y; rv:42.0) Gecko/20100101 Firefox/42.0

// Chrome UA string
// Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36
// Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36 OPR/38.0.2220.41

// Opera UA string
// Opera/9.80 (Macintosh; Intel Mac OS X; U; en) Presto/2.2.15 Version/10.00
// Opera/9.60 (Windows NT 6.0; U; en) Presto/2.1.1

// Microsoft Edge UA string
// Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36 Edg/91.0.864.59

// Safari UA string
// Mozilla/5.0 (iPhone; CPU iPhone OS 13_5_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.1.1 Mobile/15E148 Safari/604.1

// Mobile UA string
// Mozilla/5.0 (Linux; Android 10; SM-G960U) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.101 Mobile Safari/537.36
// Mozilla/5.0 (iPhone; CPU iPhone OS 14_6 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.1.1 Mobile/15E148 Safari/604.1

// Crawler and bot UA strings
// Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)
// Mozilla/5.0 (compatible; bingbot/2.0; +http://www.bing.com/bingbot.htm)
// Mozilla/5.0 (compatible; YandexAccessibilityBot/3.0; +http://yandex.com/bots)
// Mozilla/5.0 (compatible; YandexBot/3.0; +http://yandex.com/bots)

// Library and net tool UA strings
// curl/7.64.1
// PostmanRuntime/7.26.5
// Wget/1.20.3 (linux-gnu)

// Related links
// https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/User-Agent
// https://hacks.mozilla.org/2013/09/user-agent-detection-history-and-checklist/

/* var userAgent = context.Request.Headers["User-Agent"].ToString(); */
/* var browser = userAgent.Split(" ")[0]; */
/* var operatingSystem = userAgent.Split(" ")[1]; */
/* var device = userAgent.Split(" ")[2]; */
/* var application = userAgent.Split(" ")[3]; */
/* var ipAddress = context.Connection.RemoteIpAddress.ToString(); */
/* var requestPath = context.Request.Path; */
/* var requestMethod = context.Request.Method; */
/* var requestQuery = context.Request.QueryString.ToString(); */
/* var xForwardedFor = context.Request.Headers["X-Forwarded-For"].ToString(); */
/* var host = context.Request.Host.ToString(); */
/* var referer = context.Request.Headers["Referer"].ToString(); */
/* var origin = context.Request.Headers["Origin"].ToString(); */

/* Console.WriteLine($"Request: {requestMethod} {requestPath} {requestQuery}"); */
/* Console.WriteLine($"User-Agent: {userAgent}"); */
/* Console.WriteLine($"Browser: {browser}"); */
/* Console.WriteLine($"Platform: {operatingSystem}"); */
/* Console.WriteLine($"Device: {device}"); */
/* Console.WriteLine($"Application: {application}"); */
/* Console.WriteLine($"IP Address: {ipAddress}"); */
/* Console.WriteLine($"X-Forwarded-For: {xForwardedFor}"); */
/* Console.WriteLine($"Host: {host}"); */
/* Console.WriteLine($"Referer: {referer}"); */
/* Console.WriteLine($"Origin: {origin}"); */

public class HttpHeaderParser : IParser {
    public T Parse<T>(string value) {
        return JsonSerializer.Deserialize<T>(value);
    }
}