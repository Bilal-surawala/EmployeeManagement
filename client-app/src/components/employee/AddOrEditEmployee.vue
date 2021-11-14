<template>
  <el-drawer
    v-model="drawer"
    title="I am the title"
    direction="rtl"
    :before-close="handleClose"
  >
    <template v-slot:title>
      <h1>{{ employeeForm.id ? "Edit Employee" : "Add Employee" }}</h1>
    </template>

    <el-form
      ref="employeeForm"
      :model="employeeForm"
      :rules="rules"
      label-width="120px"
      class="demo-employeeForm"
    >
      <el-form-item label="First name" prop="firstName">
        <el-input v-model="employeeForm.firstName"></el-input>
      </el-form-item>
      <el-form-item label="Last name" prop="lastName">
        <el-input v-model="employeeForm.lastName"></el-input>
      </el-form-item>
      <el-form-item label="Email" prop="email">
        <el-input v-model="employeeForm.email"></el-input>
      </el-form-item>

      <el-form-item label="Gender" prop="gender">
        <el-col :span="11">
          <el-select v-model="employeeForm.gender" placeholder="Gender">
            <el-option label="Male" :value="0"></el-option>
            <el-option label="Female" :value="1"></el-option>
            <el-option label="Other" :value="2"></el-option>
          </el-select>
        </el-col>
      </el-form-item>
      <el-form-item label="Date Of Brith" required>
        <el-col :span="11">
          <el-form-item prop="dateOfBrith">
            <el-date-picker
              v-model="employeeForm.dateOfBrith"
              type="date"
              placeholder="Pick a date"
              style="width: 100%"
            ></el-date-picker>
          </el-form-item>
        </el-col>
      </el-form-item>
      <el-form-item label="Department" prop="departmentId">
        <el-col :span="11">
          <el-select
            v-model="employeeForm.departmentId"
            placeholder="Department"
          >
            <el-option
              v-for="department in departments"
              :key="department.id"
              :label="department.name"
              :value="department.id"
            ></el-option>
          </el-select>
        </el-col>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="submitForm('employeeForm')"
          >Save</el-button
        >
      </el-form-item>
    </el-form>
  </el-drawer>
</template>

<script lang="ts">
import { mapActions, mapGetters } from "vuex";
import { showMessage } from "@/common/commonMixin.js";
export default {
  mixins: [showMessage],
  props: ["employee"],
  data() {
    return {
      drawer: false,
      employeeForm: {
        firstName: null,
        lastName: null,
        email: null,
        dateOfBrith: "",
        gender: 0,
        departmentId: 1,
        photoPath: null,
      },
      rules: {
        firstName: [
          {
            required: true,
            message: "Please insert first name",
            trigger: "blur",
          },
          {
            min: 3,
            message: "Length should be 3 or more",
            trigger: "blur",
          },
        ],
        email: [
          {
            required: true,
            message: "Please insert email address",
            trigger: "blur",
          },
          {
            type: "email",
            message: "Please input correct email address",
            trigger: ["blur", "change"],
          },
        ],
        departmentId: [
          {
            required: true,
            message: "Please select Department",
            trigger: "change",
          },
        ],
        gender: [
          {
            required: true,
            message: "Please select Gender",
            trigger: "change",
          },
        ],
        dateOfBrith: [
          {
            type: "date",
            required: true,
            message: "Please pick a date",
            trigger: "change",
          },
        ],
      },
    };
  },
  computed: {
    ...mapGetters(["departments"]),
  },
  methods: {
    ...mapActions(["addOrEditEmployee"]),
    async submitForm(formName) {
      this.$refs[formName].validate(async (valid) => {
        if (valid) {
          let result = await this.addOrEditEmployee(this.employeeForm);
          if (result.success) {
            if (this.employeeForm.id) {
              this.showMessage("Detail Updated", "success");
            } else {
              this.showMessage("New Employee added", "success");
            }
            this.closeDrawer();
          }
        }
        return false;
      });
    },
    handleClose(done) {
      done();
      this.closeDrawer();
    },
    closeDrawer() {
      this.drawer = false;
      this.$emit("closeDrawer");
    },
  },
  created() {
    this.drawer = true;
    if (this.employee) {
      this.employeeForm = this.employee;
    }
  },
};
</script>