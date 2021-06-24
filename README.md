# wizload-shop
WizLoad - Shop repository based on DDD microservices

---

![shop](https://raw.githubusercontent.com/kamilagruhlke/wizload-shop/main/img/wiz-load.jpg)


---

# Architecture (In progress)

![architecture](https://raw.githubusercontent.com/kamilagruhlke/wizload-shop/main/img/architecture.png)

---

#### Identity.Api

- [x] Project initialization
- [x] Docker support
- [x] Basic facebook connection
- [x] Basic github connection

---

#### Categories.Api

- [x] Project initialization
- [x] Basic Identity EF models/domain
- [x] Docker support
- [x] Connected with Identity.api (oauth)
- [x] Connection with database
- [x] Products controller with basic actions

---

#### Products.Api
- [x] Project initialization
- [x] Basic Products EF models/domain
- [x] Docker support
- [x] Connected with Identity.api (oauth)
- [x] Connection with database
- [x] Products controller with basic actions

---

#### Basket.Api
- [x] Project initialization
- [x] Docker support
- [x] Connected with Identity.api (oauth)
- [x] Connection with redis/memcached
- [x] Basket controller with basic actions

#### Notification.Api
- [x] Project initialization
- [x] Docker support
- [x] Connected with Identity.api (oauth)
- [ ] SignalR/websockets support

#### Images.Api
- [x] Project initialization
- [x] Docker support
- [x] Connected with Identity.api (oauth)
- [x] Connection with blob storage

---

#### Wizload.Mvc
- [x] Project initialization
- [x] Docker support
- [x] Connected with Identity.api (oauth)
- [x] Connection with Categories.Api
- [x] Connection with Products.Api
- [x] Connection with Basket.Api
- [x] Connection with Images.Api
- [ ] Connection with Notification.Api (?)

#### Wizload.Spa
- [x] Project initialization
- [ ] Docker support
- [ ] Connected with Identity.api (oauth)
- [ ] Connection with Categories.Api
- [ ] Connection with Products.Api
- [ ] Connection with Basket.Api
- [ ] Connection with Images.Api
- [ ] Connection with Notification.Api (?)