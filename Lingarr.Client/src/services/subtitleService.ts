import { AxiosError, AxiosResponse, AxiosStatic } from 'axios'
import { ISubtitleService } from '@/ts'

const service = (http: AxiosStatic, resource = '/api/subtitle'): ISubtitleService => ({
    collect<T>(path: string): Promise<T> {
        return new Promise((resolve, reject) => {
            http.post(
                `${resource}/all`,
                {
                    path: path
                },
                {
                    headers: {
                        'Cache-Control': 'no-cache, no-store, must-revalidate',
                        Pragma: 'no-cache',
                        Expires: '0'
                    }
                }
            )
                .then((response: AxiosResponse<T>) => {
                    resolve(response.data)
                })
                .catch((error: AxiosError) => {
                    reject(error.response)
                })
        })
    }
})

export const subtitleService = (axios: AxiosStatic): ISubtitleService => {
    return service(axios)
}
