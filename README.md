Project Overview
Developed an AI-powered interactive experience where users can talk to historical figures, receive spoken responses, and generate AI-created illustrations featuring themselves alongside the selected legend.
This project integrates OpenAI GPT for text responses, DALL·E-3 for AI-generated illustrations, and WebRTC for real-time camera capture, creating a unique and engaging experience.

Features
 Conversational AI with Historical Legends:
•	Users can ask real-time questions to historical figures, such as Socrates, Isaac Newton, and Suleiman the Magnificent.
•	AI generates responses in their unique tone using OpenAI GPT.
•	The responses can be displayed as text or converted into speech (Text-to-Speech integration).
Speech-to-Text & Text-to-Speech Integration:
•	Users can speak their questions aloud instead of typing (Speech-to-Text).
•	The AI’s responses can be played aloud using Web Speech API (Text-to-Speech).
AI-Generated Illustrations of the User & Legend:
•	Users can take a real-time photo with their webcam or upload an image.
•	The system analyzes facial features and integrates them into a semi-minimalist cartoon-style illustration using OpenAI’s DALL·E-3 API.
•	The AI-generated image features the user standing next to the selected historical figure in a relevant setting.
QR Code Generation for Easy Sharing:
•	Once the AI-generated image is ready, users can generate a QR code.
•	This QR code allows users to easily share their AI-generated illustration on social media or download it to their phones.
Web Deployment on Azure:
•	The entire project is deployed on Azure, ensuring scalability and accessibility for users worldwide.
________________________________________
Technology Stack
 Frontend:
•	HTML, CSS, Bootstrap – Responsive and dynamic UI design.
•	JavaScript (WebRTC & Web Speech API) – Camera integration, speech-to-text, and text-to-speech processing.
Backend:
•	C#, ASP.NET MVC – Handles user interactions and AI communication.
•	OpenAI GPT API – Generates AI responses based on user input.
•	OpenAI DALL·E-3 API – Creates AI-generated illustrations of the user with the selected legend.
Additional AI & Interactive Features:
•	Speech-to-Text & Text-to-Speech API – Enables spoken interaction with the AI.
•	QR Code API (api.qrserver.com) – Generates QR codes for AI-generated images.
Deployment & Hosting:
•	Microsoft Azure App Service – Hosts the web application for global access.
________________________________________
Key Takeaways
Through this project, I:
•	Implemented multi-modal AI interactions, including text, speech, and images.
•	Developed real-time webcam capture and AI-driven image generation.
•	Created an interactive user experience by integrating speech recognition and text-to-speech.
•	Added QR code functionality for seamless image sharing.
•	Successfully deployed the entire project on Azure.

Project Link:	https://fikir1-eyh5g5hegjh4a5az.canadacentral-01.azurewebsites.net/



