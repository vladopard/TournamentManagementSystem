# Tournament Management System

An ASP.NET Core 8 Web API for managing tournaments, teams, players, matches and statistics, featuring EF Core, FluentValidation, AutoMapper and JWT authentication.

## Features
- **Authentication & Authorization**: ASP.NET Identity with JWT tokens  
- **CRUD Operations**: Tournaments, Teams, Players, Matches, Player Match Stats  
- **Paging & Sorting**: Built‑in support on list endpoints  
- **Validation**: FluentValidation on all incoming DTOs  
- **Mapping**: AutoMapper profiles for entity⇄DTO conversion  
- **Error Handling**: Global exception middleware for consistent JSON errors  
- **Swagger**: Interactive API documentation

## Tech Stack
- **Framework**: .NET 8.0, ASP.NET Core 8  
- **ORM**: Entity Framework Core, PostgreSQL  
- **Validation**: FluentValidation  
- **Mapping**: AutoMapper  
- **Auth**: ASP.NET Identity, JWT Bearer  
- **Containerization**: Dockerfile (multi‑stage), Kubernetes manifests  
- **Documentation**: Swagger / OpenAPI

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)  
- [PostgreSQL](https://www.postgresql.org/)  
- [Docker](https://www.docker.com/) & [kubectl](https://kubernetes.io/docs/tasks/tools/) (optional)

### Local Development
### Running with Kubernetes Locally

#### Prerequisites
- Docker  
- `kubectl` (v1.26+)  
- A local k8s cluster ([kind] or [minikube])

### 1. Build and tag your local image
   ```bash
from repo root
docker build -t tms-api:local .
### 2. Create (or switch to) your local cluster
Using kind
kind create cluster --name tms-dev
Using minikube
minikube start
eval $(minikube docker-env)

#### 3. Load the image into the cluster
  kind
  kind load docker-image tms-api:local --name tms-dev
  minikube
  No step needed if you ran eval $(minikube docker-env).

4. Deploy all Kubernetes manifests
bash
Copy
Edit
# apply Postgres, API & Service, plus any PVC/ConfigMap
kubectl apply -f k8s/
5. Verify pods & services
bash
Copy
Edit
kubectl get pods
kubectl get svc
You should see:

postgres pod + service

tms-api Deployment + ClusterIP service

6. Port‑forward to test locally
bash
Copy
Edit
# API on localhost:5000
kubectl port-forward svc/tms-api 5000:80
Now open:

bash
Copy
Edit
http://localhost:5000/swagger/index.html
The API will recreate, migrate & seed the database on startup.
