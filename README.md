# JohnyCalendarGenerator

JohnyCalendarGenerator is a tool designed to automate the process of converting training schedules, typically received as Excel table screenshots, into calendar events compatible with iOS devices. This eliminates the manual task of inputting each event individually.

## Features

- **Image Processing**: Utilizes Optical Character Recognition (OCR) to extract text from training schedule images.
- **Data Parsing**: Interprets the extracted text to identify and structure training events.
- **Calendar Integration**: Generates an `.ics` file that can be imported into iOS calendars, seamlessly adding all training events.

## Prerequisites

- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)

## Setup and Usage

### 1. Clone the Repository

```bash
git clone https://github.com/mabargiel/JohnyCalendarGenerator.git
```

### 2. Navigate to the Project Directory

```bash
cd JohnyCalendarGenerator
```

### 3. Prepare Your Training Schedule Image

- Place the image file (e.g., `training_schedule.jpg`) in the `input` directory within the project folder.

### 4. Configure `docker-compose.yml`

- Open the `docker-compose.yml` file in a text editor.
- Modify the environment variables to specify your input image and desired output paths:

```yaml
environment:
  IMAGE_PATH: "/app/input/training_schedule.jpg"
  ICS_PATH: "/app/output/training_calendar.ics"
```

### 5. Build and Run the Docker Container

```bash
docker-compose up --build
```

### 6. Retrieve the Generated Calendar File

- After the process completes, locate the `training_calendar.ics` file in the `output` directory.

### 7. Import the `.ics` File into Your iOS Calendar

- Transfer the `.ics` file to your iOS device via email, cloud storage, or any preferred method.
- Open the file on your device and follow the prompts to add the events to your calendar.

## Example

Below is a sample training schedule image processed by JohnyCalendarGenerator:

![Sample Training Schedule](https://github.com/mabargiel/JohnyCalendarGenerator/blob/main/sample_training_schedule.jpg)

The corresponding events are extracted and compiled into an `.ics` file for seamless calendar integration.

## Contributing

Contributions are welcome! Feel free to open issues or submit pull requests to enhance the functionality of this project.

## License

This project is licensed under the [MIT License](LICENSE).

---

*Note: Ensure that the training schedule images are clear and legible to facilitate accurate text extraction by the OCR process.*
