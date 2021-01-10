# wizload-shop
WizLoad - Shop repository based on DDD microservices

---

#### Identity.Api

- [x] Project initialization
- [x] Docker support
- [x] Basic facebook connection
- [x] Basic github connection

---

#### Categories.Api

- [x] Project initialization
- [ ] Basic Identity EF models/domain
- [x] Docker support
- [x] Connected with Identity.api (oauth)
- [ ] Connection with database
- [ ] Products controller with basic actions

---

#### Products.Api
- [x] Project initialization
- [ ] Basic Products EF models/domain
- [x] Docker support
- [x] Connected with Identity.api (oauth)
- [ ] Connection with database
- [ ] Products controller with basic actions

---

#### Basket.Api
- [x] Project initialization
- [ ] Basic Identity EF models/domain
- [x] Docker support
- [x] Connected with Identity.api (oauth)
- [ ] Connection with redis/memcached
- [ ] Basket controller with basic actions

#### Notification.Api
- [x] Project initialization
- [x] Docker support
- [x] Connected with Identity.api (oauth)
- [ ] SignalR/websockets support

---

#### Wizload.Mvc or Wizload.SPA
- [x] Project initialization
- [x] Docker support
- [x] Connected with Identity.api (oauth)
- [ ] Connection with Identity.Api
- [ ] Connection with Categories.Api
- [ ] Connection with Products.Api
- [ ] Connection with Basket.Api
