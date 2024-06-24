<template>
    <VListItem>
        <p>
            {{ message.content }}
        </p>
    </VListItem>
</template>

<script setup lang="ts">
import { Message } from '@/models/Message';

    const props = defineProps({
        message: {
            type: Message,
            required: true
        }
    })
</script>

<style scoped>

</style>