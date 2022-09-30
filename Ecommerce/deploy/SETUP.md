


`k3d cluster create ecommerce --registry-create ecommerce-registry --port "8088:8088@loadbalancer" --port "18080:8080@loadbalancer" --port "18080:8080@loadbalancer" --servers 1 --agents 2`

