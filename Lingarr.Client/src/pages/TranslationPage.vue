﻿<template>
    <PageLayout>
        <div class="w-full">
            <!-- Search and Filters -->
            <div class="bg-tertiary flex flex-wrap items-center justify-between gap-2 p-4">
                <SearchComponent v-model="filter" />
                <div class="flex w-full justify-between space-x-2 md:w-fit">
                    <button
                        v-if="isSelectMode"
                        class="border-accent text-primary-content hover:text-primary-content/50 cursor-pointer rounded-md border px-2 py-1 transition-colors"
                        @click="handleDelete">
                        {{ translate('translations.delete') }}
                        ({{ translationRequestStore.selectedRequests.length }})
                    </button>
                    <button
                        class="border-accent text-primary-content hover:text-primary-content/50 cursor-pointer rounded-md border px-2 py-1 transition-colors"
                        @click="toggleSelectMode">
                        {{
                            isSelectMode
                                ? translate('translations.cancel')
                                : translate('translations.select')
                        }}
                    </button>
                    <SortControls
                        v-model="filter"
                        :options="[
                            {
                                label: 'Sort by Added',
                                value: 'CreatedAt'
                            },
                            {
                                label: 'Sort by Completed',
                                value: 'CompletedAt'
                            },
                            {
                                label: 'Sort by Title',
                                value: 'Title'
                            }
                        ]" />
                </div>
            </div>

            <div class="w-full px-4">
                <div class="border-accent hidden border-b font-bold md:grid md:grid-cols-12">
                    <div class="col-span-4 px-4 py-2">
                        {{ translate('translations.title') }}
                    </div>
                    <div class="col-span-1 px-4 py-2">{{ translate('translations.source') }}</div>
                    <div class="col-span-1 px-4 py-2">{{ translate('translations.target') }}</div>
                    <div class="col-span-1 px-4 py-2">{{ translate('translations.status') }}</div>
                    <div class="px-4 py-2" :class="isSelectMode ? 'col-span-2' : 'col-span-3'">
                        {{ translate('translations.progress') }}
                    </div>
                    <div class="col-span-1 px-4 py-2">
                        {{ translate('translations.completed') }}
                    </div>
                    <div class="col-span-1 flex justify-end px-4 py-2">
                        <ReloadComponent @toggle:update="translationRequestStore.fetch()" />
                    </div>
                    <div
                        v-if="isSelectMode"
                        class="col-span-1 flex items-center justify-center px-4 py-2">
                        <CheckboxComponent
                            :model-value="translationRequestStore.selectAll"
                            @change="translationRequestStore.toggleSelectAll()" />
                    </div>
                </div>
                <div
                    v-for="item in translationRequests.items"
                    :key="item.id"
                    class="md:border-accent rounded-lg py-4 shadow-sm md:grid md:grid-cols-12 md:rounded-none md:border-b md:bg-transparent md:p-0 md:shadow-none">
                    <div class="deletable float-right h-5 w-5 md:hidden">
                        <TranslationAction
                            :status="item.status"
                            @toggle:action="(action) => handleAction(item, action)" />
                    </div>
                    <div class="mb-2 md:col-span-4 md:mb-0 md:px-4 md:py-2">
                        <span :id="`deletable-${item.id}`" class="font-bold md:hidden">
                            {{ translate('translations.title') }}:&nbsp;
                        </span>
                        <span
                            v-if="item.mediaType === MEDIA_TYPE.EPISODE"
                            v-show-title
                            class="cursor-help"
                            :title="item.title">
                            {{ item.title }}
                        </span>
                        <span v-else>
                            {{ item.title }}
                        </span>
                    </div>
                    <div class="mb-2 md:col-span-1 md:mb-0 md:px-4 md:py-2">
                        <span class="font-bold md:hidden">
                            {{ translate('translations.source') }}:&nbsp;
                        </span>
                        <BadgeComponent classes="text-primary-content border-accent bg-secondary">
                            {{ item.sourceLanguage.toUpperCase() }}
                        </BadgeComponent>
                    </div>
                    <div class="mb-2 md:col-span-1 md:mb-0 md:px-4 md:py-2">
                        <span class="font-bold md:hidden">
                            {{ translate('translations.target') }}:&nbsp;
                        </span>
                        <BadgeComponent classes="text-primary-content border-accent bg-secondary">
                            {{ item.targetLanguage.toUpperCase() }}
                        </BadgeComponent>
                    </div>
                    <div class="mb-2 md:col-span-1 md:mb-0 md:px-4 md:py-2">
                        <span class="font-bold md:hidden">
                            {{ translate('translations.status') }}:&nbsp;
                        </span>
                        <TranslationStatus :translation-status="item.status" />
                    </div>
                    <div
                        class="mb-2 flex items-center md:mb-0 md:px-4 md:py-2"
                        :class="isSelectMode ? 'md:col-span-2' : 'md:col-span-3'">
                        <div
                            v-if="item.status === TRANSLATION_STATUS.INPROGRESS && item.progress"
                            class="w-full">
                            <span class="mr-2 font-bold md:hidden">
                                {{ translate('translations.progress') }}:&nbsp;
                            </span>
                            <TranslationProgress :progress="item.progress" />
                        </div>
                    </div>
                    <div class="mb-2 md:col-span-1 md:mb-0 md:px-4 md:py-2">
                        <span class="font-bold md:hidden">
                            {{ translate('translations.completed') }}:&nbsp;
                        </span>
                        <TranslationCompletedAt
                            v-if="item.completedAt"
                            :completed-at="item.completedAt" />
                    </div>
                    <div
                        class="hidden items-center justify-between md:col-span-1 md:flex md:justify-end md:px-4 md:py-2">
                        <div class="flex h-5 w-5 items-center justify-center">
                            <TranslationAction
                                :status="item.status"
                                @toggle:action="(action) => handleAction(item, action)" />
                        </div>
                    </div>
                    <div
                        v-if="isSelectMode"
                        class="col-span-1 flex items-center justify-end py-2 md:justify-center md:px-4">
                        <CheckboxComponent
                            :model-value="
                                translationRequestStore.selectedRequests.some(
                                    (request) => request.id === item.id
                                )
                            "
                            @change="translationRequestStore.toggleSelect(item)" />
                    </div>
                </div>
            </div>
            <PaginationComponent
                v-if="translationRequests.totalCount"
                v-model="filter"
                :total-count="translationRequests.totalCount"
                :page-size="translationRequests.pageSize" />
        </div>
    </PageLayout>
</template>

<script setup lang="ts">
import { ref, onMounted, onUnmounted, ComputedRef, computed } from 'vue'
import {
    Hub,
    IFilter,
    IPagedResult,
    ITranslationRequest,
    MEDIA_TYPE,
    TRANSLATION_STATUS
} from '@/ts'
import { useTranslationRequestStore } from '@/store/translationRequest'
import { useSignalR } from '@/composables/useSignalR'
import useDebounce from '@/composables/useDebounce'
import PaginationComponent from '@/components/common/PaginationComponent.vue'
import SortControls from '@/components/common/SortControls.vue'
import SearchComponent from '@/components/common/SearchComponent.vue'
import ReloadComponent from '@/components/common/ReloadComponent.vue'
import TranslationStatus from '@/components/common/TranslationStatus.vue'
import TranslationProgress from '@/components/common/TranslationProgress.vue'
import TranslationAction from '@/components/common/TranslationAction.vue'
import TranslationCompletedAt from '@/components/common/TranslationCompletedAt.vue'
import BadgeComponent from '@/components/common/BadgeComponent.vue'
import PageLayout from '@/components/layout/PageLayout.vue'
import CheckboxComponent from '@/components/common/CheckboxComponent.vue'

const signalR = useSignalR()
const hubConnection = ref<Hub>()
const translationRequestStore = useTranslationRequestStore()

const translationRequests: ComputedRef<IPagedResult<ITranslationRequest>> = computed(
    () => translationRequestStore.getTranslationRequests
)

const filter: ComputedRef<IFilter> = computed({
    get: () => translationRequestStore.filter,
    set: useDebounce((value: IFilter) => {
        translationRequestStore.setFilter(value)
    }, 300)
})

function handleAction(translationRequest: ITranslationRequest, action: string) {
    if (!action) return
    if (action === 'cancel') {
        return translationRequestStore.cancel(translationRequest)
    }
    return translationRequestStore.remove(translationRequest)
}

onMounted(async () => {
    await translationRequestStore.fetch()
    hubConnection.value = await signalR.connect(
        'TranslationRequests',
        '/signalr/TranslationRequests'
    )

    await hubConnection.value.joinGroup({ group: 'TranslationRequests' })
    hubConnection.value.on('RequestProgress', translationRequestStore.updateProgress)
})

onUnmounted(async () => {
    hubConnection.value?.off('RequestProgress', translationRequestStore.updateProgress)
})

const isSelectMode = ref(false)

const toggleSelectMode = () => {
    isSelectMode.value = !isSelectMode.value
    if (!isSelectMode.value) {
        translationRequestStore.clearSelection()
    }
}

const handleDelete = async () => {
    for (const request of translationRequestStore.getSelectedRequests) {
        await translationRequestStore.remove(request)
    }
    translationRequestStore.clearSelection()
    translationRequestStore.fetch()
}
</script>
