🧠 AI-Enhanced Interactive Experience with Historical Legends
🧩 Project Overview

This project presents an AI-powered interactive web experience where users can converse with historical figures, receive spoken responses, and generate AI-created illustrations featuring themselves alongside the selected legend.

It integrates multiple AI technologies — OpenAI GPT for conversational text generation, DALL·E-3 for image creation, and WebRTC for real-time photo capture — creating a unique and immersive experience that blends speech, visuals, and interactivity.

⚙️ Features
🗣️ Conversational AI with Historical Legends

Users can ask real-time questions to historical figures such as Socrates, Isaac Newton, and Suleiman the Magnificent.

AI generates context-aware responses in each character’s distinct tone using OpenAI GPT.

Responses can be displayed as text or played aloud via Text-to-Speech.

🎙️ Speech-to-Text & Text-to-Speech Integration

Users can speak their questions aloud using Speech-to-Text functionality.

The AI’s responses can be played aloud through the Web Speech API (Text-to-Speech).

🧍‍♀️ AI-Generated Illustrations (User + Legend)

Users can take a real-time webcam photo or upload an existing image.

The system analyzes facial features and combines them with OpenAI’s DALL·E-3 to produce a semi-minimalist cartoon-style illustration.

The final image depicts the user standing beside the chosen historical figure in a relevant historical setting.

🔗 QR Code Generation for Easy Sharing

Once the AI-generated image is ready, users can generate a QR code for quick sharing.

The QR code allows users to share or download their AI-generated illustration seamlessly.

☁️ Web Deployment on Azure

The entire system is hosted on Microsoft Azure App Service, ensuring global accessibility and scalability.

🛠️ Technology Stack
Layer	Technologies Used
Frontend	HTML, CSS, Bootstrap (responsive UI), JavaScript (WebRTC, Web Speech API)
Backend	C#, ASP.NET MVC
AI Integration	OpenAI GPT (text), OpenAI DALL·E-3 (image generation)
Additional Features	Speech-to-Text, Text-to-Speech, QR Code API (api.qrserver.com)
Hosting	Microsoft Azure App Service
🧠 Key Takeaways

Through this project, I:

Implemented multi-modal AI interactions (text, speech, image).

Built real-time webcam capture and AI-driven illustration generation workflows.

Created an engaging, voice-enabled user interface.

Integrated QR code generation for simple image sharing.

Successfully deployed a cloud-based AI experience on Azure.

⚠️ Important Note – OpenAI API Key Required

To run this project, you must provide a valid OpenAI API Key.

After obtaining your key:

Open the file PhotoController.cs
Replace the empty quotes in the following line with your API key:

private readonly string _openAIApiKey = "";


Open appsettings.json
Enter the same key between the quotes:

"ApiKey": ""


⚡ Do not share or commit your API key publicly. Use environment variables or User Secrets for local security.

🔗 Project Access

🌐 Live Demo:
https://fikir1-eyh5g5hegjh4a5az.canadacentral-01.azurewebsites.net/
