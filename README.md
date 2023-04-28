# Privasight

## What is Privasight ? 
Privasight is an application that allows users to view their data collected by social media companies with ease. The application is avaialbe here https://hayley-kwok.github.io/Privasight/.

https://user-images.githubusercontent.com/47830627/235015422-1834d30b-0bab-42f3-bb9c-1a56b3ef898c.mp4

## Privasight Aim
Privasight aims to allow anyone to understand their copy of data returned by different companies. With the right of access protected by the GDPR, users can request a copy of their data. But the returned data are usually in various data formats and sometimes even incorrectly encoded.
Privasight aims to remove the technical barrier for everyone and ease the understanding and analysis of their data.

## How it all started ?
I came across this [video](https://www.youtube.com/watch?v=hLjht9uJWgw&t=2s) about how much data Google stores about an individual years ago, and the result shocked me. 
I then did some digging into the data collected by different social media companies and realised that the amount of data collected was a lot more than I expected. 
The data format is usually JSON, but very often data is scattered in different files, and it is really difficult to have a general idea of what is being collected and how much data is being collected. 
And that is the problem Privasight tries to solve.

This concept lingered in my mind for two years until the time came to choose my dissertation topic. I took that opportunity to turn this idea into reality.
Without the strong motivation from the tight deadlines at uni, this idea may honestly never came to reality.

## Technical Background of the Application
In my head, I always envision Privasight as a desktop application because it just doesn't make sense for it to be a web app. 
Downloading all data from one company and uploading it to a random website just raises a lot of concerns and makes Privasight no different from all the other companies collecting tonnes of data from users.

Originally, I planned to use .NET MAUI for the development, but due to the delay of the official release, I have to use something else. 
As a .NET lover, I tried to find something within .NET that would allow me to migrate to .NET MAUI when it becomes more mature. 
That is when I discovered .NET Blazor WASM. Technically, it is a framework for website, but it also allows me to do all the processing within the client browser. 
Of course, the processing power of this is very limited, but this will also be a good starting point for users to test out the application before downloading it.

The future development plan for Privasight is to slowly migrate to .NET MAUI application with full support on the desktop versions. 
The web version will stay as a taster version for users to try the application out before downloading the desktop version.

There is a lot more formal justification for the technical decisions made for Privasight, and they are all documented in my dissertation report. 
I will slowly move the important decisions I made from my report to here, but in the meantime, feel free to take a look at the report here to see how it all works.

## Contribute to the application
Despite how much I want to continue the development of Privasight, I have limited time for it. Feel free to raise any PRs to continue the development and solve any issues.
