class DarkModeManager {
    constructor() {
        this.themeToggle = document.getElementById('themeToggle');
        this.currentTheme = localStorage.getItem('theme') || 'light';
        this.init();
    }

    init() {
        this.applyTheme(this.currentTheme);
        
        this.themeToggle.addEventListener('click', () => this.toggleTheme());
        
        this.watchSystemPreference();
        
        console.log('Dark Mode Manager initialisé - Thème:', this.currentTheme);
    }

    applyTheme(theme) {
        document.documentElement.setAttribute('data-theme', theme);
        localStorage.setItem('theme', theme);
        this.updateToggleIcon(theme);
        this.currentTheme = theme;
        
        window.dispatchEvent(new CustomEvent('themeChanged', { detail: theme }));
    }

    toggleTheme() {
        const newTheme = this.currentTheme === 'light' ? 'dark' : 'light';
        this.applyTheme(newTheme);
    }

    updateToggleIcon(theme) {
        const moonIcon = this.themeToggle.querySelector('.fa-moon');
        const sunIcon = this.themeToggle.querySelector('.fa-sun');
        
        if (theme === 'dark') {
            moonIcon.classList.add('d-none');
            sunIcon.classList.remove('d-none');
            this.themeToggle.title = 'Basculer en mode clair';
        } else {
            moonIcon.classList.remove('d-none');
            sunIcon.classList.add('d-none');
            this.themeToggle.title = 'Basculer en mode sombre';
        }
    }

    watchSystemPreference() {
        const mediaQuery = window.matchMedia('(prefers-color-scheme: dark)');
        
        if (!localStorage.getItem('theme')) {
            this.applyTheme(mediaQuery.matches ? 'dark' : 'light');
        }
        
        mediaQuery.addEventListener('change', (e) => {
            if (!localStorage.getItem('theme')) {
                this.applyTheme(e.matches ? 'dark' : 'light');
            }
        });
    }
}

document.addEventListener('DOMContentLoaded', () => {
    new DarkModeManager();
});

window.DarkModeManager = DarkModeManager;