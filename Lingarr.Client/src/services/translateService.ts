import { AxiosError, AxiosResponse, AxiosStatic } from 'axios'
import { ILanguage, ISubtitle, ITranslateService, MediaType } from '@/ts'

const service = (http: AxiosStatic, resource = '/api/translate'): ITranslateService => ({
    translateSubtitle<T>(
        mediaId: number,
        subtitle: ISubtitle,
        source: string,
        target: ILanguage,
        mediaType: MediaType
    ): Promise<T> {
        return new Promise((resolve, reject) => {
            http.post(`${resource}/subtitle`, {
                mediaId: mediaId,
                subtitlePath: subtitle.path,
                sourceLanguage: source,
                targetLanguage: target.code,
                mediaType: mediaType
            })

                .then((response: AxiosResponse<T>) => {
                    resolve(response.data)
                })
                .catch((error: AxiosError) => {
                    reject(error.response)
                })
        })
    },
    getLanguages<T>(): Promise<T> {
        return new Promise((resolve, reject) => {
            http.get(`${resource}/languages`)
                .then((response: AxiosResponse<T>) => {
                    resolve(response.data)
                })
                .catch((error: AxiosError) => {
                    reject(error.response)
                })
        })
    }
})

export const translateService = (axios: AxiosStatic): ITranslateService => {
    return service(axios)
}
