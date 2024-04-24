# LLMChatProject
Just a kind of pet-project, chatting service with LLMs based on microservice architecture

Used technologies:
- Clean architecture
- EF
- DI
- MediatR + validation pipeline, which uses:
- FluentValidation
- Mapster
- JWT authentication & authorization
Microservices:
- ChatWebAPI: responsible for communication with LLMs and storing messages and chats data
  Uses MySQL database
- AuthenticationWebAPI: handles authentication and stores users credetials in:
  MsSQL database
- ApiGateway: uses Ocelot to merge and hide the backend part, leaving a single api endpoint
