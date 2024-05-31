<template>
    <v-sheet class="bg pa-12" rounded>
        <v-card class="container ">
            <v-form>
                <v-text-field
            class="mb-2"
            label="Email"   
            clearable
            ></v-text-field>

            <v-text-field
            label="Password"
            placeholder="Enter your password"
            clearable
            ></v-text-field>

            <br>

            <v-btn
            color="success"
            size="large"
            type="submit"
            variant="elevated"
            block
            >
            Sign In
            </v-btn>
            </v-form>
        </v-card>
    </v-sheet>
    
</template>

<script setup lang="ts">

</script>

<style scoped>
    .container {
        width: 100%;
        max-width: 500px;
        min-width: 400px;
        margin-right: 1rem;
        margin-left: 1rem;
        padding: 1.5em 2em;
    }
</style>