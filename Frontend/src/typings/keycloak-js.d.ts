declare module 'keycloak-js' {
    interface KeycloakInitOptions {
        onLoad?: 'login-required' | 'check-sso';
        checkLoginIframe?: boolean;
        pkceMethod?: 'S256';
        flow?: 'standard' | 'implicit' | 'hybrid';
        enableLogging?: boolean;
    }

    interface KeycloakProfile {
        username?: string;
        firstName?: string;
        lastName?: string;
        email?: string;
        [key: string]: any;
    }

    export default class Keycloak {
        constructor(config?: any);
        init(options?: KeycloakInitOptions): Promise<boolean>;
        login(): void;
        logout(options?: any): void;
        register(): void;
        accountManagement(): void;
        hasRealmRole(role: string): boolean;
        hasResourceRole(role: string, resource?: string): boolean;
        loadUserProfile(): Promise<KeycloakProfile>;
        token?: string;
        tokenParsed?: any;
        refreshToken?: string;
        subject?: string;
        authenticated?: boolean;
    }
}
