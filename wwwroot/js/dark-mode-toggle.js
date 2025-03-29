document.addEventListener("DOMContentLoaded", () => {
  const toggleButton = document.getElementById("dark-mode-toggle");
  const darkModeIcon = document.getElementById("dark-mode-icon");
  const body = document.body;

  // Helper function to update the icon
  const updateIcon = () => {
    if (body.classList.contains("dark-mode")) {
      darkModeIcon.classList.remove("bi-moon");
      darkModeIcon.classList.add("bi-sun");
    } else {
      darkModeIcon.classList.remove("bi-sun");
      darkModeIcon.classList.add("bi-moon");
    }
  };

  // Load the user's preference from localStorage
  if (localStorage.getItem("darkMode") === "enabled") {
    body.classList.add("dark-mode");
    updateIcon();
  }

  toggleButton.addEventListener("click", () => {
    if (body.classList.contains("dark-mode")) {
      body.classList.remove("dark-mode");
      localStorage.setItem("darkMode", "disabled");
    } else {
      body.classList.add("dark-mode");
      localStorage.setItem("darkMode", "enabled");
    }
    updateIcon();
  });
});
