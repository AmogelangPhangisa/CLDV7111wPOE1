(function () {
  console.log("Service Worker script loaded");

  self.addEventListener("install", (event) => {
    console.log("Service Worker installed");
  });

  self.addEventListener("activate", (event) => {
    console.log("Service Worker activated");
  });

  self.addEventListener("push", (event) => {
    console.log("Push event received:", event);
    const data = event.data ? event.data.json() : {};
    const options = {
      body: data.message || "New notification",
      icon: '/images/logo-k.svg',
      image: 'https://st10464558.azurewebsites.net/images/product-items-4.jpeg',
      vibrate: [200, 100, 200],
      tag: data.tag || 'new-notification'
    };

    event.waitUntil(
      self.registration.showNotification(data.title || "Notification", options)
    );
  });
})();
