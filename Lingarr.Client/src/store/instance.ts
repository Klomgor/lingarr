import { acceptHMRUpdate, defineStore } from 'pinia'
import { IUseInstanceStore, ITheme, THEMES, IMovie, IShow } from '@/ts'
import { useLocalStorage } from '@/composables/useLocalStorage'
import services from '@/services'

const localStorage = useLocalStorage()

export const useInstanceStore = defineStore({
    id: 'instance',
    state: (): IUseInstanceStore => ({
        isOpen: false,
        theme: THEMES.LINGARR,
        poster: ''
    }),
    getters: {
        getTheme: (state): ITheme => state.theme,
        getIsOpen: (state): boolean => state.isOpen,
        getPoster: (state): string => state.poster
    },
    actions: {
        setIsOpen(isOpen: boolean): void {
            this.isOpen = isOpen
        },
        setPoster({ content, type }: { content: IMovie | IShow; type: string }): void {
            if (!content || !Array.isArray(content.media)) {
                this.poster = ''
                return
            }
            const posterMedia = content.media.find((media) => media.type === 'poster')
            this.poster = posterMedia ? `${type}${posterMedia.path}` : ''
        },
        async applyThemeOnLoad(): Promise<void> {
            let theme = localStorage.getItem<ITheme>('theme')
            theme = (await services.setting.getSetting<ITheme>('theme')) ?? theme
            this.setTheme(theme)
        },
        setTheme(theme: ITheme): void {
            localStorage.setItem('theme', theme)
            this.theme = theme
        },
        storeTheme(theme: ITheme): void {
            services.setting.setSetting('theme', theme)
            this.setTheme(theme)
        }
    }
})

if (import.meta.hot) {
    import.meta.hot.accept(acceptHMRUpdate(useInstanceStore, import.meta.hot))
}