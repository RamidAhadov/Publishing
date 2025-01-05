# Prometheus Metrics Publisher (C# Application)

## Overview
This C# application is designed to publish custom metrics to Prometheus, providing real-time insights into application performance and health. It integrates seamlessly with Prometheus by exposing an HTTP endpoint that Prometheus scrapes for metrics.

## Features
- **Custom Metrics** – Define and publish custom application metrics.
- **HTTP Endpoint** – Exposes metrics on an endpoint that Prometheus can scrape.
- **Lightweight and Fast** – Minimal overhead for maximum performance.
- **Easy Integration** – Easily integrates with existing C# applications.
- **Configurable Intervals** – Define special intervals for publishing metrics.

## Requirements
- **.NET SDK** (Version 6 or higher)
- **Prometheus** (Running locally or in Docker)
- **Docker (Optional)** – For containerized deployment

## Installation
1. **Clone the Repository**
   ```bash
   git clone https://github.com/username/repository.git
   cd repository
   ```

2. **Restore Dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the Application**
   ```bash
   dotnet build
   ```

4. **Run the Application**
   ```bash
   dotnet run
   ```

## Usage
Once the application is running, the metrics will be exposed at the following endpoint:
```
http://localhost:5000/metrics
```
Prometheus can be configured to scrape this endpoint by adding the following job to your Prometheus configuration:
```yaml
scrape_configs:
  - job_name: 'csharp_application'
    static_configs:
      - targets: ['localhost:5000']
```

## Configuration
You can configure the application by modifying the `appsettings.json` file:
```json
{
  "RequestInterval": {
    "TimeInterval": [100,1000],
    "IncInterval": [1, 5],
    "DecInterval": [50,500],
    "UpperBound": 2000,
    "RequestLimit": -1
  },
  "PrometheusMetrics": {
    "Metrics": [
      {
        "Id": 1,
        "MetricType": "Counter",
        "MetricName": "http_requests_total",
        "Help": "Total number of HTTP requests",
        "Labels": ["method", "endpoint", "status_code"],
        "NeedsRequestInterval": true
      },
      {
        "Id": 2,
        "MetricType": "Gauge",
        "MetricName": "cpu_usage",
        "Help": "Current CPU usage percentage",
        "Labels": ["host"],
        "NeedsRequestInterval": true
      },
      {
        "Id": 3,
        "MetricType": "Histogram",
        "MetricName": "request_duration_seconds",
        "Help": "Duration of HTTP requests",
        "Labels": ["method", "endpoint"],
        "Buckets": [0.1, 0.5, 1, 5, 10],
        "NeedsRequestInterval": false
      }
    ]
  },
  "Intervals": {
    "MetricIntervals": [
      {
        "MetricId": 1,
        "MetricType": "Counter",
        "TimeInterval": [100,1000],
        "IncInterval": [1, 5],
        "RequestLimit": -1
      },
      {
        "MetricId": 2,
        "MetricType": "Gauge",
        "TimeInterval": [100,1000],
        "IncInterval": [1, 5],
        "DecInterval": [50,500],
        "UpperBound": 2000,
        "RequestLimit": -1
      }
    ]
  }
}
```

## Metrics Example
Example of a custom metric:
```
# HELP application_requests_total Total number of requests
# TYPE application_requests_total counter
application_requests_total{method="GET"} 100
```

## Docker Deployment (Optional)
1. **Build Docker Image**
   ```bash
   docker build -t csharp-metrics-app .
   ```
2. **Run Docker Container**
   ```bash
   docker run -p 5000:5000 csharp-metrics-app
   ```
   

## License
This project is licensed under the MIT License. See the `LICENSE` file for details.

## Contributing
Contributions are welcome! Please open an issue or submit a pull request.

## Contact
For questions or support, feel free to contact [Your Name] at [Your Email].

