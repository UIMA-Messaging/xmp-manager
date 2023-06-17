# XMP Manager

The XMP Manager is responsible for the communication between the backend system and the XMPP host. In the context of UIMA, an Ejabberd is the XMPP host. This service is responsible for communicating new user registrations as well as dereigstrations to the XMPP host. More specifically, this service communicates via the Ejabberd API.

> More information regarding the Ejabberd rest API can be found [here](https://docs.ejabberd.im/developer/ejabberd-api/). Please note that the `XMPP host` is not an external service, but rather an image ran along side the system that needs manual setup.

This service is part of the greater UIMA project that communicates exclusively via an event bus. This service has its own database for XMP user data:

![Individual  UIMA C4 Model](https://github.com/UIMA-Messaging/xmp-manager/assets/56337726/7824a122-3012-4086-8c9d-35c1e0704142)

## Configuration
You can configure environment variable in appsettings.json file in the project. This file should look like:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Bugsnag": {
    "ApiKey": "BUGSANG_API_KEY"
  },
  "Ejabberd": {
    "BaseUrl": "BASE_URL",
    "Host": "HOST",
    "Service": "HOST",
    "Username": "USERNAME",
    "Password": "PASSWORD"
  },
  "RabbitMQ": {
    "Uri": "RABBITMQ_HOST",
    "Exchange": "THIS_EXCHANGE",
    "UserRegistrations": {
      "Exchange": "REGISTRATION_EXCHANGE",
      "RegistrationsRoutingKey": "ROUTING_KEY",
      "UnregistrationsRoutingKey": "ROUTING_KEY"
    }
  }
}
```

## Event bus subscriptions

Routing key `users.new`

This subscription is responsible for creating what is know as an jabber account, an internal account for chat communication.

Message body:
```json
{
  "id": "string",
  "jid": "string",
  "displayName": "string",
  "username": "string",
  "image": "string",
  "ephemeralPassword": "string",
  "joinedAt": "2023-06-17T12:40:45.223Z",
  "editedAt": "2023-06-17T12:40:45.223Z"
}
```

Routing key `users.remove`

This subscription is responsible to removing an jabber account from the database

```json
{
  "jid": "string"
}
```

Please see the Registration Service for more information regarding the event bus messages [here](https://github.com/UIMA-Messaging/registration-service).
